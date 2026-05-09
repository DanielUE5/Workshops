using DI.Injectors;
using DI.Modules;

namespace DI
{
    public class DInjector
    {
        public static Injector CreateInjector(IModule module)
        {
            module.Configure();
            return new Injector(module);
        }
    }
}