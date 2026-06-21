using Common;
using System;
using UnityEngine;

public class SOContainerContainer : MonoBehaviour, ILoadingSceneEntity
{
    private Action<UnityEngine.Object> _onReady;

    void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
    {
        _onReady = onReady;
    }

    void Start()
    {
        _onReady.Invoke(this);
    }

}
