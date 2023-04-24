using System;

namespace Scripts.Core.EventBus
{
    [Serializable]
    public class WeakSubscription<T> : IDisposable where T : WeakMessage
    {
        private Action<T> listener;
        public WeakSubscription(Action<T> listener)
        {
            this.listener = listener;
            WeakEventBus.Subscribe<T>(this.listener);
        }
        public void Dispose()
        {
            WeakEventBus.Unsubscribe<T>(this.listener);
        }
    }
}