using System;

namespace Common
{
    // Boilerplate to implementing classes:
    /* 
    private Action<UnityEngine.Object> _onReady;

    void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
    {
        _onReady = onReady;
    }
    */
    public interface ILoadingSceneEntity
    {
        void OnCreation(Action<UnityEngine.Object> onReady);
    }
}
