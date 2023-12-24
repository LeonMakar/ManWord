using System;

public interface IContainer
{
    void Register<TService, TImplamentation>() where TImplamentation : TService;

    TService Resolve<TService, TImplamentation>(params Type[] typesToInject);
    object Resolve(Type service, Type implamentation, params Type[] typesToInject);

}
