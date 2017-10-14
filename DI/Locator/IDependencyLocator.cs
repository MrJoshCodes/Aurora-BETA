namespace AuroraEmu.DI.Locator
{
    public interface IDependencyLocator
    {
        T Resolve<T>();
    }
}
