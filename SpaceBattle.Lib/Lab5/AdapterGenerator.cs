using System;
using System.Collections.Generic;
using System.Reflection;
using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab5;

public static class AdapterGenerator
{
    public static T CreateAdapter<T>(IDictionary<string, object> obj) where T : class
    {
        return (T)DynamicProxy.CreateProxy(typeof(T), obj);
    }
}

public class DynamicProxy : DispatchProxy
{
    private IDictionary<string, object> _store = null!;
    private Type _interfaceType = null!;

    public static object CreateProxy(Type interfaceType, IDictionary<string, object> store)
    {
        var method = typeof(DispatchProxy)
            .GetMethod(nameof(DispatchProxy.Create), BindingFlags.Public | BindingFlags.Static, null, Type.EmptyTypes, null)?
            .MakeGenericMethod(interfaceType, typeof(DynamicProxy));

        if (method == null)
        {
            throw new InvalidOperationException("не удалось однозначно разрешить метод DispatchProxy.Create");
        }

        object proxy = method.Invoke(null, null)!;

        ((DynamicProxy)proxy)._store = store ?? throw new ArgumentNullException(nameof(store));
        ((DynamicProxy)proxy)._interfaceType = interfaceType ?? throw new ArgumentNullException(nameof(interfaceType));

        return proxy;
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        if (targetMethod == null) return null;

        string methodName = targetMethod.Name;

        if (methodName.StartsWith("get_"))
        {
            string propName = methodName.Substring(4);
            var propInfo = _interfaceType.GetProperty(propName);

            var adapterAttr = propInfo?.GetCustomAttribute<AdapterAttribute>();
            if (adapterAttr != null)
            {
                return IoC.Resolve<object>(adapterAttr.StrategyName, _store);
            }

            if (_store.TryGetValue(propName, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException($"свойство '{propName}' не найдено в конфигурации объекта");
        }
        
        if (methodName.StartsWith("set_"))
        {
            string propName = methodName.Substring(4);
            if (args != null && args.Length > 0)
            {
                _store[propName] = args[0]!;
            }
            return null;
        }

        return null;
    }
}