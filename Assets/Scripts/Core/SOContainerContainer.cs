using Common;
using Core.Generated;
using System;
using UnityEngine;

public class SOContainerContainer : MonoBehaviour, ILoadingSceneEntity
{
    private Action<UnityEngine.Object> _onReady;

    [SerializeField] private IngameLevelRequirementSOContainer ingameLevelRequirementSOContainer;
    public static IngameLevelRequirementSOContainer IngameLevelRequirementSOContainer { get; set; }

    void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
    {
        _onReady = onReady;
    }

    void Start()
    {
        IngameLevelRequirementSOContainer = ingameLevelRequirementSOContainer;
        _onReady.Invoke(this);
    }

}
