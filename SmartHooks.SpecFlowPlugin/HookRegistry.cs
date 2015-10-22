using System;
using System.Collections.Generic;

namespace SmartHooks.SpecFlowPlugin
{
    internal class HookRegistry: IHookRegistry
    {
        readonly IDictionary<Type, IHookHandler> _registry = new Dictionary<Type, IHookHandler>();
        public void RegisterHookHandler(Type bindingType, IHookHandler hookHandler)
        {
            _registry.Add(bindingType, hookHandler);
        }

        public void UnregisterHookHandler(Type bindingType)
        {
            _registry.Remove(bindingType);
        }

        public IHookHandler FindHookHandler(Type bindingType)
        {
            IHookHandler result;
            _registry.TryGetValue(bindingType, out result);
            return result;
        }
    }
}