using System.Collections.Concurrent;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlSugar.FluentMapping
{
    public static class SqlSugarFluentMapping
    {
        // Cache: key = (entityClrType, ctorParamType) -> factory: object param -> EntityTypeBuilder<T> as object
        private static readonly ConcurrentDictionary<(Type, Type), Func<object, object>> s_builderFactories = new();

        /// <summary>
        /// Apply the fluent mappings to the SqlSugar client
        /// </summary>
        /// <param name="sqlSugarInstance">SQLSugar instance</param>
        /// <param name="mappings">Mapping classes to apply</param>
        public static void ApplyFluentMapping(this ISqlSugarClient sqlSugarInstance, params object[] mappings)
        {
            // check ConfigureExternalServices if null and initialize
            sqlSugarInstance.CurrentConnectionConfig.ConfigureExternalServices ??= new ConfigureExternalServices();

            // create a dictionary of entity type to mapping instance
            var mapDict = mappings.ToDictionary(
                m => m.GetType().BaseType!.GetGenericArguments()[0],
                m => WrapMapping(m)
            );

            // configure ES at entity level (ex: table name mapping)
            sqlSugarInstance.CurrentConnectionConfig.ConfigureExternalServices.EntityNameService = (type, entityInfo) =>
            {
                // get mapping for declaring type
                if (!mapDict.TryGetValue(type, out var mapInstance))
                    return;

                var factory = GetBuilderFactory(type, entityInfo);
                var entityBuilder = factory(entityInfo);

                mapInstance.Configure(entityBuilder!);
            };

            // configure ES at property level (ex: column name mapping)
            sqlSugarInstance.CurrentConnectionConfig.ConfigureExternalServices.EntityService = (type, entityInfo) =>
            {
                // is property part of a declaring type?
                if (type.DeclaringType == null)
                    return;

                // get mapping for declaring type
                if (!mapDict.TryGetValue(type.DeclaringType, out var mapInstance))
                    return;

                // get builder factory for declaring type
                var factory = GetBuilderFactory(type.DeclaringType, entityInfo);
                var entityBuilder = factory(entityInfo);
                mapInstance.Configure(entityBuilder!);
            };
        }

        private static Func<object, object> GetBuilderFactory(Type entityClrType, object? ctorArg)
        {
            // determine the runtime type of the ctor argument (may be null)
            var argType = ctorArg?.GetType();

            // if we have a concrete runtime type use that as part of the key so we can reuse a compatible factory
            var keyParamType = argType ?? typeof(object);
            var key = (entityClrType, keyParamType);

            return s_builderFactories.GetOrAdd(key, k => CreateFactoryFor(entityClrType, argType));
        }

        private static Func<object, object> CreateFactoryFor(Type entityClrType, Type? runtimeArgType)
        {
            // build the closed generic builder type
            var builderType = typeof(EntityTypeBuilder<>).MakeGenericType(entityClrType);

            // find a constructor to use. Prefer one whose parameter type is assignable from the runtimeArgType (if provided).
            var ctors = builderType.GetConstructors();
            ConstructorInfo? chosen = null;

            if (runtimeArgType != null)
            {
                chosen = ctors
                    .Select(c => c.GetParameters().FirstOrDefault())
                    .Where(p => p != null && p.ParameterType.IsAssignableFrom(runtimeArgType))
                    .Select(p => p!.Member as ConstructorInfo)
                    .FirstOrDefault();
            }

            // fallback strategies
            if (chosen == null)
            {
                // prefer an EntityInfo param if present
                chosen = ctors
                    .Select(c => (ctor: c, p: c.GetParameters().FirstOrDefault()))
                    .Where(x => x.p != null && x.p.ParameterType.Name.Contains("EntityInfo"))
                    .Select(x => x.ctor)
                    .FirstOrDefault();
            }

            if (chosen == null)
            {
                // take first ctor that has a single parameter
                chosen = ctors.FirstOrDefault(c => c.GetParameters().Length == 1);
            }

            if (chosen == null)
                throw new InvalidOperationException($"No suitable constructor found for {builderType.FullName}");

            var paramType = chosen.GetParameters()[0].ParameterType;

            // build a Func<object, object> where the object parameter is converted to the ctor param type
            var arg = Expression.Parameter(typeof(object), "arg");
            var converted = Expression.Convert(arg, paramType);
            var newExpr = Expression.New(chosen, converted);
            var body = Expression.Convert(newExpr, typeof(object));

            var lambda = Expression.Lambda<Func<object, object>>(body, arg);
            return lambda.Compile();
        }

        private static IEntityMapping WrapMapping(object mappingInstance)
        {
            var entityType = mappingInstance.GetType().BaseType!.GetGenericArguments()[0];
            var wrapperType = typeof(EntityMappingWrapper<>).MakeGenericType(entityType);
            return (IEntityMapping)Activator.CreateInstance(wrapperType, mappingInstance)!;
        }
    }
}
