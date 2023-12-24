﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Injector
{
    private IContainer _container;

    public Injector(IContainer container)
    {
        _container = container;
    }

    public static Injector Instance;
    public Dictionary<Type, object> SingletonServices { get; } = new Dictionary<Type, object>();

    //private Dictionary<Type, object> _temporaryServices = new Dictionary<Type, object>();

    //private Dictionary<Type, Dictionary<Type, object>> _singletonServices = new Dictionary<Type, Dictionary<Type, object>>();

    private Dictionary<Type, Dictionary<Type, object>> _repeatedServices = new Dictionary<Type, Dictionary<Type, object>>();

    public bool CheckAvailabilitySingletoneServiceInInjector(Type type)
    {
        if (SingletonServices.ContainsKey(type))
            return true;
        else return false;
    }

    public bool CheckAvailabilityRepeatedServiceInInjector(Type service, Type implamentation)
    {
        if (_repeatedServices.ContainsKey(service))
        {
            if (_repeatedServices[service].ContainsKey(implamentation))
                return true;
            return false;
        }
        return false;

    }


    public void BuildSingletoneService<TService, TImplamentation>(params Type[] typesToResolve) where TImplamentation : TService
    {
        if (SingletonServices.ContainsKey(typeof(TService)))
            throw new InvalidOperationException($"{typeof(TImplamentation)} almost exist in Dictionary");
        else
        {
            SingletonServices[typeof(TService)] = _container.Resolve<TService, TImplamentation>(typesToResolve);
        }
    }
    public object Resolve(Type serviceType, Type implamentationType, params Type[] typesToInject)
    {
        return _container.Resolve(serviceType, implamentationType, typesToInject);
    }

    public void BuildRepeatedService<TService, TImplamentation>(params Type[] typesToResolve) where TImplamentation : TService
    {
        if (_repeatedServices.ContainsKey(typeof(TService)))
        {
            if (_repeatedServices[typeof(TService)].ContainsKey(typeof(TImplamentation)))
            {
                //_temporaryServices[typeof(TService)][typeof(TImplamentation)] = _container.Resolve<TService, TImplamentation>(typesToResolve);
                throw new InvalidOperationException($"{typeof(TService)} as {typeof(TImplamentation)} exist in repeatedService dictionary");
            }
            else
                _repeatedServices[typeof(TService)].Add(typeof(TImplamentation), _container.Resolve<TService, TImplamentation>(typesToResolve));
        }
        else
            _repeatedServices.Add(typeof(TService), new Dictionary<Type, object>()
            {
                [typeof(TService)] = _container.Resolve<TService, TImplamentation>(typesToResolve)
            });
    }

    public void AddExistingSingletoneService<TService, TImplamentation>(object currentObject) where TImplamentation : TService
    {
        if (SingletonServices.ContainsKey(typeof(TService)))
            throw new InvalidOperationException($"{typeof(TImplamentation)} almost exist in Dictionary");
        else
            SingletonServices[typeof(TService)] = currentObject;
    }
    //public void AddExistingTemporaryService<TService, TImplamentation>(object currentObject) where TImplamentation : TService
    //{
    //    if (_repeatedServices.ContainsKey(typeof(TService)))
    //    {
    //        if (_repeatedServices[typeof(TService)].ContainsKey(typeof(TImplamentation)))
    //            _repeatedServices[typeof(TService)][typeof(TImplamentation)] = currentObject;
    //        else
    //        { }
    //    }
    //    else
    //    {
    //    }
    //}

    public TService GetSingletoneService<TService, TImplamentation>() where TImplamentation : TService
    {
        if (SingletonServices.ContainsKey(typeof(TService)))
            return (TService)SingletonServices[typeof(TService)];
        else
            throw new InvalidOperationException($"{typeof(TService)} dosnt register in singletone dictionary for {typeof(TImplamentation)}");
    }
    public object GetSingletoneService(Type service)
    {
        if (SingletonServices.ContainsKey(service))
            return SingletonServices[service];
        else
            throw new InvalidOperationException($"{service} dosnt register in singletone dictionary for {service}");
    }

    public TService GetRepeatedService<TService, TImplamentation>() where TImplamentation : TService
    {
        if (_repeatedServices.ContainsKey(typeof(TService)))
        {
            if (_repeatedServices[typeof(TService)].ContainsKey(typeof(TImplamentation)))
                return (TService)_repeatedServices[typeof(TService)][typeof(TImplamentation)];
            else
                throw new InvalidOperationException($"{typeof(TImplamentation)} dosnt register in repeatedService dictionary");
        }
        else
            throw new InvalidOperationException($"{typeof(TService)} dosnt register in repeatedService dictionary");
    }
    public object GetRepeatedService(Type service, Type implamentation)
    {
        if (_repeatedServices.ContainsKey(service))
        {
            if (_repeatedServices[service].ContainsKey(implamentation))
                return _repeatedServices[service][implamentation];
            else
                throw new InvalidOperationException($"{implamentation} dosnt register in repeatedService dictionary");
        }
        else
            throw new InvalidOperationException($"{service} dosnt register in repeatedService dictionary");
    }

    //public void ScanInjectFields()
    //{
    //    foreach (var item in SingletonServices)
    //    {
    //        if (item.Value is IInjectable)
    //        {
    //            Type type = item.Value.GetType();
    //            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
    //            foreach (var method in methods)
    //            {
    //                var trueMethods = method.GetCustomAttribute(typeof(InjectAttribute), true);
                    
    //                if (trueMethods?.GetType() == typeof(InjectAttribute))
    //                {
    //                    var parameters = method.GetParameters();
    //                    object[] values = new object[parameters.Length];
    //                    int i = 0;
    //                    foreach (var parameter in parameters)
    //                    {
    //                        values[i] = SingletonServices.First(f => f.Key.Name == parameter.ParameterType.Name).Value;
    //                        i++;
    //                    }
    //                    method.Invoke(item.Value, values);
    //                }
    //            }
    //        }
    //    }
    //}
    //public void Injecting(object objectThatNeedInjecting)
    //{
    //    if(objectThatNeedInjecting is IInjectable)
    //    {
    //        Type type = objectThatNeedInjecting.GetType();
    //        var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
    //        foreach (var method in methods)
    //        {
    //            var trueMethods = method.GetCustomAttribute(typeof(InjectAttribute), true);

    //            if (trueMethods?.GetType() == typeof(InjectAttribute))
    //            {
    //                var parameters = method.GetParameters();
    //                object[] values = new object[parameters.Length];
    //                int i = 0;
    //                foreach (var parameter in parameters)
    //                {
    //                    values[i] = SingletonServices.First(f => f.Key.Name == parameter.ParameterType.Name).Value;
    //                    i++;
    //                }
    //                method.Invoke(objectThatNeedInjecting, values);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        throw new InvalidOperationException("Object is not IInjectable");
    //    }
    //}
}

//public static class InjectorExtansions
//{
//    public static IInjectable Injecting(this IInjectable currentService)
//    {
//        Injector injector = Injector.Instance;
//        List<IService> services = new List<IService>();
//        foreach (var service in currentService.ServiceAndImplamentation)
//        {
//            if (injector.CheckAvailabilitySingletoneServiceInInjector(service.Key))
//                services.Add((IService)injector.GetSingletoneService(service.Key));
//            else
//            {
//                if (injector.CheckAvailabilityRepeatedServiceInInjector(service.Key, service.Value))
//                    services.Add((IService)injector.GetRepeatedService(service.Key, service.Value));
//                else
//                {
//                    var iService = (IService)injector.Resolve(service.Key, service.Value);
//                    if (iService is IInjectable iInjectable)
//                        iInjectable.Injecting();
//                    services.Add(iService);
//                }
//            }
//        }
//        currentService.Inject(services.ToArray());
//        return currentService;
//    }
//}


public interface IInjectable
{
}
