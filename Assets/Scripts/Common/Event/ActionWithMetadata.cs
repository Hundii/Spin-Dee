using System;
using UnityEngine;

namespace Common
{
    public class ActionWithMetadata<T>
    {
        public int order;
        public bool oneTimeUse;
        private Action<T> action;
        public bool isDead;
        public ActionWithMetadata(Action<T> action, int order = 0, bool oneTimeUse = false)
        {
            this.action = action;
            this.order = order;
            this.oneTimeUse = oneTimeUse;
        }
        public ActionWithMetadata(Action<T> action, bool oneTimeUse = false) : this(action, 0, oneTimeUse)
        {
        }

        public ActionWithMetadata(Action<T> action) : this(action, 0)
        {
        }

        public ActionWithMetadata(Action action, int order = 0, bool oneTimeUse = false)
        {
            this.action = (_) => action();
            this.order = order;
            this.oneTimeUse = oneTimeUse;
        }

        public ActionWithMetadata(Action action, bool oneTimeUse = false) : this(action, 0, oneTimeUse)
        {
        }
        public ActionWithMetadata(Action action) : this(action, 0)
        {
        }

        public void Invoke(T data)
        {
            action?.Invoke(data);

            if (oneTimeUse)
            {
                Kill();
            }
        }

        public void Kill()
        {
            action = null;
            isDead = true;
        }
    }
}
