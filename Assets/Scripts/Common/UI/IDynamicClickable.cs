using System;
using UnityEngine.EventSystems;

namespace Common
{
    public interface IDynamicClickable<T> : IPointerClickHandler
    {
        public GameEvent<T> OnClick { get; set; }
    }
}
