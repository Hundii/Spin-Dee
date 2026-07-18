using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class GameEvent
    {
        private List<ActionWithMetadata<object>> callbacks = new();

        public bool HasBeenInvoked { get; private set; } = false;

        public IDisposable RegisterListener(ActionWithMetadata<object> callback)
        {
            callbacks.Add(callback);
            callbacks = callbacks.OrderBy(x => x.order).ToList();
            return new Subscription<object>(this, callback);
        }
        public void UnRegisterListener(ActionWithMetadata<object> callback)
        {
            callbacks.Remove(callback);
        }


        public void Invoke()
        {
            HasBeenInvoked = true;

            foreach (var callback in callbacks)
            {
                callback.Invoke(null);
            }

            callbacks.RemoveAll(x => x.isDead);
        }

        public void Reset()
        {
            HasBeenInvoked = false;

            ResetListeners();
        }
        public void ResetListeners()
        {
            callbacks = new();
        }
    }

    public class GameEvent<T>
    {
        private List<ActionWithMetadata<T>> callbacks = new();

        public T PreviousValue { get; private set; } = default;
        public bool HasPreviousValue { get; private set; }

        public T CurrentValue { get; private set; } = default;
        public bool HasCurrentValue { get; private set; }

        public IDisposable RegisterListener(ActionWithMetadata<T> callback)
        {
            callbacks.Add(callback);
            callbacks = callbacks.OrderBy(x => x.order).ToList();
            return new Subscription<T>(this, callback);
        }
        public void UnRegisterListener(ActionWithMetadata<T> callback)
        {
            callbacks.Remove(callback);
        }


        public void Invoke(T value)
        {
            PreviousValue = CurrentValue;
            CurrentValue = value;
            HasPreviousValue = HasCurrentValue;
            HasCurrentValue = true;

            foreach (var callback in callbacks)
            {
                callback.Invoke(value);
            }

            callbacks.RemoveAll(x => x.isDead);
        }

        public void Reset()
        {
            PreviousValue = default;
            HasPreviousValue = false;

            CurrentValue = default;
            HasCurrentValue = false;
            ResetListeners();
        }
        public void ResetListeners()
        {
            callbacks = new();
        }
    }
}
