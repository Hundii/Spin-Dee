using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SubscriptionList
    {
        private readonly List<IDisposable> subscriptions = new();

        public void Add(IDisposable subscription)
        {
            subscriptions.Add(subscription);
        }

        public void UnsubscribeAll()
        {
            foreach (var subscription in subscriptions)
            {
                subscription.Dispose();
            }
      
            subscriptions.Clear();
        }
    }
}
