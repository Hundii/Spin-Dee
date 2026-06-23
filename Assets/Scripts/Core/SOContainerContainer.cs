using Common;
using Core.Generated;
using System;
using UnityEngine;

public class SOContainerContainer : MonoBehaviour, ILoadingSceneEntity
{
    private Action<UnityEngine.Object> _onReady;

    [SerializeField] private IngameLevelRequirementSOContainer ingameLevelRequirementSOContainer;
    public static IngameLevelRequirementSOContainer IngameLevelRequirementSOContainer { get; set; }

    [SerializeField] private CurrencySOContainer currencySOContainer;
    public static CurrencySOContainer CurrencySOContainer { get; set; }

    [SerializeField] private StatSOContainer statSOContainer;
    public static StatSOContainer StatSOContainer { get; set; }

    [SerializeField] private ItemDefinitionSOContainer itemDefinitionSOContainer;
    public static ItemDefinitionSOContainer ItemDefinitionSOContainer { get; set; }

    [SerializeField] private CapsuleStatsSOContainer capsuleStatsSOContainer;
    public static CapsuleStatsSOContainer CapsuleStatsSOContainer { get; set; }

    [SerializeField] private MicrobeStatsSOContainer microbeStatsSOContainer;
    public static MicrobeStatsSOContainer MicrobeStatsSOContainer { get; set; }

    [SerializeField] private HarvesterStatsSOContainer harvesterStatsSOContainer;
    public static HarvesterStatsSOContainer HarvesterStatsSOContainer { get; set; }

    [SerializeField] private MicrobeSOContainer microbeSOContainer;
    public static MicrobeSOContainer MicrobeSOContainer { get; set; }

    [SerializeField] private LiquidSOContainer liquidSOContainer;
    public static LiquidSOContainer LiquidSOContainer { get; set; }

    void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
    {
        _onReady = onReady;
    }

    void Start()
    {
        IngameLevelRequirementSOContainer = ingameLevelRequirementSOContainer;
        CurrencySOContainer = currencySOContainer;
        StatSOContainer = statSOContainer;
        ItemDefinitionSOContainer = itemDefinitionSOContainer;
        CapsuleStatsSOContainer = capsuleStatsSOContainer;
        MicrobeStatsSOContainer = microbeStatsSOContainer;
        HarvesterStatsSOContainer = harvesterStatsSOContainer;
        LiquidSOContainer = liquidSOContainer;
        MicrobeSOContainer = microbeSOContainer;
        _onReady.Invoke(this);
    }

}
