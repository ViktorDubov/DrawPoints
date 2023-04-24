namespace Scripts.Core.EventBus
{
    public abstract class WeakMessage
    {
        public void Send<T>() where T : WeakMessage
        {
            WeakEventBus.Request<T>(this as T);
        }
    }
}

