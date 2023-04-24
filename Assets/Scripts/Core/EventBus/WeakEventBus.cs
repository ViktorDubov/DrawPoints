using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.EventBus
{
    public static class WeakEventBus
    {
        private static Dictionary<Type, List<object>> executors;
        private static Dictionary<Type, List<object>> Executors
        {
            get
            {
                if(executors == null)
                {
                    executors = new Dictionary<Type, List<object>>();
                }
                return executors;
            }
        }

        public static void Request<T>(T data) where T : WeakMessage
        {
            if (Executors.TryGetValue(typeof(T), out List<object> handlers))
            {
                for (int i = 0; i < handlers.Count; i++)
                {
                    if (handlers[i] is Action<T> action)
                    {
                        action.Invoke(data);
                    }
                }
            }
        }

        public static void Subscribe<T>(Action<T> listener) where T : WeakMessage
        {
            if (!Executors.TryGetValue(typeof(T), out List<object> handlers))
            {
                handlers = new List<object>();
                Executors.Add(typeof(T), handlers);
            }
            handlers.Add(listener);
        }

        public static void Unsubscribe<T>(Action<T> listener) where T : WeakMessage
        {
            if (Executors.TryGetValue(typeof(T), out List<object> handlers))
            {
                handlers.Remove(listener);
            }
        }
    }
}