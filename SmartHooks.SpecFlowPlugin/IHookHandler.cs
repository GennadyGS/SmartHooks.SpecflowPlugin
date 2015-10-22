namespace SmartHooks.SpecFlowPlugin
{
    public interface IHookHandler
    {
        void BeforeStep();

        void AfterStep();
    }
}