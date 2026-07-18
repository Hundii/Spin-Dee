using System;
using UnityEngine;

namespace Common
{
    public class Subscription<T> : IDisposable
    {
        private readonly GameEvent<T> gameEvent;
        private readonly GameEvent gameEventWithoutParameter;
        private readonly ActionWithMetadata<T> callback;

        public Subscription(GameEvent<T> gameEvent, ActionWithMetadata<T> callback)
        {
            this.gameEvent = gameEvent;
            this.callback = callback;
        }

        public Subscription(GameEvent gameEvent, ActionWithMetadata<T> callback)
        {
            gameEventWithoutParameter = gameEvent;
            this.callback = callback;
        }

        public void Dispose()
        {
            gameEvent?.UnRegisterListener(callback);
            gameEventWithoutParameter?.UnRegisterListener(callback as ActionWithMetadata<object>);
        }
    }
}
