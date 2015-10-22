using System;

namespace SmartHooks.SpecFlowPlugin
{
    public interface IHookRegistry
    {
        void RegisterHookHandler(Type bindingType, IHookHandler hookHandler);

        void UnregisterHookHandler(Type bindingType);

        IHookHandler FindHookHandler(Type bindingType);
    }
}