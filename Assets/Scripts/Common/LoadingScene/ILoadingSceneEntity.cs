using System;

namespace Common
{
    // Boilerplate to implementing classes:
    /* 
    private Action<ILoadingSceneEntity> _onReady;

    void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
    {
        _onReady = onReady;
    }
    */
    public interface ILoadingSceneEntity
    {
        void OnCreation(Action<ILoadingSceneEntity> onReady);
    }
}
