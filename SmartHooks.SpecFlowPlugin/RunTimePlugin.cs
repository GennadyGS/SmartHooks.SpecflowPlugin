using BoDi;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Infrastructure;

namespace SmartHooks.SpecFlowPlugin
{
    public class RunTimePlugin : IRuntimePlugin
    {
        public void RegisterDependencies(ObjectContainer container)
        {
            container.RegisterTypeAs<BindingInvoker, IBindingInvoker>();
            container.RegisterTypeAs<HookRegistry, IHookRegistry>();
        }

        public void RegisterCustomizations(ObjectContainer container, RuntimeConfiguration runtimeConfiguration)
        {
        }

        public void RegisterConfigurationDefaults(RuntimeConfiguration runtimeConfiguration)
        {
        }
    }
}