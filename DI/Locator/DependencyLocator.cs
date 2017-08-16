using Autofac;

namespace AuroraEmu.DI.Locator
{
    public class DependencyLocator : IDependencyLocator
    {
        T IDependencyLocator.Resolve<T>()
        {
            return Engine.Container.Resolve<T>();
        }
    }
}
