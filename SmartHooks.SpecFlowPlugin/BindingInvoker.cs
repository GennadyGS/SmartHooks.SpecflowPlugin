using System;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.ErrorHandling;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Tracing;

namespace SmartHooks.SpecFlowPlugin
{
    public class BindingInvoker : IBindingInvoker
    {
        private readonly IHookRegistry _hookRegistry;
        private readonly TechTalk.SpecFlow.Bindings.BindingInvoker _targetInvoker;

        public BindingInvoker(RuntimeConfiguration runtimeConfiguration,
            IErrorProvider errorProvider,
            IHookRegistry hookRegistry)
        {
            _hookRegistry = hookRegistry;
            _targetInvoker = new TechTalk.SpecFlow.Bindings.BindingInvoker(runtimeConfiguration, errorProvider);
        }

        public object InvokeBinding(IBinding binding, IContextManager contextManager, object[] arguments, ITestTracer testTracer, out TimeSpan duration)
        {
            CheckInvokeBeforeStepHook(binding);
            var invokeBinding = _targetInvoker.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
            CheckInvokeAfterStepHook(binding);
            return invokeBinding;
        }

        private void CheckInvokeBeforeStepHook(IBinding binding)
        {
            var hookHandler = FindHookHandler(binding);
            if (hookHandler != null)
            {
                hookHandler.BeforeStep();
            }
        }

        private void CheckInvokeAfterStepHook(IBinding binding)
        {
            var hookHandler = FindHookHandler(binding);
            if (hookHandler != null)
            {
                hookHandler.AfterStep();
            }
        }

        private IHookHandler FindHookHandler(IBinding binding)
        {
            Type bindingType = TryGetBindingType(binding);
            if (bindingType == null)
            {
                return null;
            }
            return _hookRegistry.FindHookHandler(bindingType);
        }

        private static Type TryGetBindingType(IBinding binding)
        {
            StepDefinitionBinding stepDefinitionBinding = binding as StepDefinitionBinding;
            if (stepDefinitionBinding == null)
            {
                return null;
            }
            var type = stepDefinitionBinding.Method.Type;
            RuntimeBindingType runtimeBindingType = type as RuntimeBindingType;
            if (runtimeBindingType == null)
            {
                return null;
            }
            return runtimeBindingType.Type;
        }
    }
}