using System;

namespace Common
{
    public class GameEvent
    {
        private Action onEventInvoked;
        private Action onEventParameterlessInvokedOneTime;

        public bool HasBeenInvoked { get; private set; }

        public void RegisterListener(Action callback, bool callImmediatelyIfHasBeenInvoked = false)
        {
            onEventInvoked += callback;
            if (callImmediatelyIfHasBeenInvoked && HasBeenInvoked)
            {
                callback?.Invoke();
            }
        }
        public void RegisterOneTimeListener(Action callback)
        {
            onEventParameterlessInvokedOneTime += callback;
        }
        public void UnRegisterListener(Action callback)
        {
            onEventInvoked -= callback;
        }
        public void UnRegisterOneTimeListener(Action callback)
        {
            onEventParameterlessInvokedOneTime -= callback;
        }

        public void Invoke()
        {
            InvokeOneTimeListeners();
            onEventInvoked?.Invoke();
            HasBeenInvoked = true;
        }

        private void InvokeOneTimeListeners()
        {
            onEventParameterlessInvokedOneTime?.Invoke();
            onEventParameterlessInvokedOneTime = null;
        }

        public void Reset()
        {
            HasBeenInvoked = false;
            ResetListeners();
        }

        public void ResetListeners()
        {
            onEventInvoked = null;
            onEventParameterlessInvokedOneTime = null;
        }

        public static GameEvent operator +(GameEvent gameEvent, Action callback)
        {
            gameEvent.RegisterListener(callback);
            return gameEvent;
        }

        public static GameEvent operator -(GameEvent gameEvent, Action callback)
        {
            gameEvent.UnRegisterListener(callback);
            return gameEvent;
        }
    }

    public class GameEvent<T>
    {
        private Action<T> onEventInvoked;
        private Action onEventParameterlessInvoked;

        private Action<T> onOneTimeEventInvoked;
        private Action onOneTimeEventParameterlessInvoked;

        public T PreviousValue { get; private set; } = default;
        public bool HasPreviousValue { get; private set; }

        public T CurrentValue { get; private set; } = default;
        public bool HasCurrentValue { get; private set; }

        public void RegisterListener(Action<T> callback, bool callImmediatelyIfHasValue = false)
        {
            onEventInvoked += callback;
            if (callImmediatelyIfHasValue && HasCurrentValue)
            {
                callback.Invoke(CurrentValue);
            }
        }
        public void RegisterListener(Action callback, bool callImmediatelyIfHasValue = false)
        {
            onEventParameterlessInvoked += callback;
            if (callImmediatelyIfHasValue && HasCurrentValue)
            {
                callback.Invoke();
            }
        }
        public void UnRegisterListener(Action<T> callback)
        {
            onEventInvoked -= callback;
        }
        public void UnRegisterListener(Action callback)
        {
            onEventParameterlessInvoked -= callback;
        }

        public void RegisterOneTimeListener(Action<T> callback)
        {
            onOneTimeEventInvoked += callback;
        }
        public void RegisterOneTimeListener(Action callback)
        {
            onOneTimeEventParameterlessInvoked += callback;
        }
        public void UnRegisterOneTimeListener(Action<T> callback)
        {
            onOneTimeEventInvoked -= callback;
        }
        public void UnRegisterOneTimeListener(Action callback)
        {
            onOneTimeEventParameterlessInvoked -= callback;
        }

        public void Invoke(T value)
        {
            PreviousValue = CurrentValue;
            CurrentValue = value;
            HasPreviousValue = HasCurrentValue;
            HasCurrentValue = true;

            InvokeOneTimeListeners(value);

            onEventInvoked?.Invoke(value);
            onEventParameterlessInvoked?.Invoke();
        }

        private void InvokeOneTimeListeners(T value)
        {
            onOneTimeEventInvoked?.Invoke(value);
            onOneTimeEventParameterlessInvoked?.Invoke();

            onOneTimeEventInvoked = null;
            onOneTimeEventParameterlessInvoked = null;
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
            onEventInvoked = null;
            onEventParameterlessInvoked = null;

            onOneTimeEventInvoked = null;
            onOneTimeEventParameterlessInvoked = null;
        }
        public static GameEvent<T> operator +(GameEvent<T> gameEvent, Action<T> callback)
        {
            gameEvent.RegisterListener(callback);
            return gameEvent;
        }

        public static GameEvent<T> operator -(GameEvent<T> gameEvent, Action<T> callback)
        {
            gameEvent.UnRegisterListener(callback);
            return gameEvent;
        }

        public static GameEvent<T> operator +(GameEvent<T> gameEvent, Action callback)
        {
            gameEvent.RegisterListener(callback);
            return gameEvent;
        }

        public static GameEvent<T> operator -(GameEvent<T> gameEvent, Action callback)
        {
            gameEvent.UnRegisterListener(callback);
            return gameEvent;
        }

    }
}
