using Common;
using Core.Generated;
using System;
using UnityEngine;

public class SOContainerContainer : MonoBehaviour, ILoadingSceneEntity, IPersistentManager
{
    private Action<ILoadingSceneEntity> _onReady;

    [SerializeField] private IngameLevelRequirementSOContainer ingameLevelRequirementSOContainer;
    public static IngameLevelRequirementSOContainer IngameLevelRequirementSOContainer { get; set; }

    [SerializeField] private CurrencySOContainer currencySOContainer;
    public static CurrencySOContainer CurrencySOContainer { get; set; }

    [SerializeField] private StatSOContainer statSOContainer;
    public static StatSOContainer StatSOContainer { get; set; }

    [SerializeField] private ItemDefinitionSOContainer itemDefinitionSOContainer;
    public static ItemDefinitionSOContainer ItemDefinitionSOContainer { get; set; }

    [SerializeField] private CapsuleSOContainer capsuleSOContainer;
    public static CapsuleSOContainer CapsuleSOContainer { get; set; }

    [SerializeField] private MicrobeStatsSOContainer microbeStatsSOContainer;
    public static MicrobeStatsSOContainer MicrobeStatsSOContainer { get; set; }

    [SerializeField] private HarvesterStatsSOContainer harvesterStatsSOContainer;
    public static HarvesterStatsSOContainer HarvesterStatsSOContainer { get; set; }

    [SerializeField] private MicrobeSOContainer microbeSOContainer;
    public static MicrobeSOContainer MicrobeSOContainer { get; set; }

    [SerializeField] private LiquidSOContainer liquidSOContainer;
    public static LiquidSOContainer LiquidSOContainer { get; set; }

    [SerializeField] private BoosterSOContainer boosterSOContainer;
    public static BoosterSOContainer BoosterSOContainer { get; set; }

    [SerializeField] private JarSOContainer jarSOContainer;
    public static JarSOContainer JarSOContainer { get; set; }

    void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
    {
        _onReady = onReady;
    }

    private void Awake()
    {
        IngameLevelRequirementSOContainer = ingameLevelRequirementSOContainer;
        CurrencySOContainer = currencySOContainer;
        StatSOContainer = statSOContainer;
        ItemDefinitionSOContainer = itemDefinitionSOContainer;
        CapsuleSOContainer = capsuleSOContainer;
        MicrobeStatsSOContainer = microbeStatsSOContainer;
        HarvesterStatsSOContainer = harvesterStatsSOContainer;
        LiquidSOContainer = liquidSOContainer;
        MicrobeSOContainer = microbeSOContainer;
        BoosterSOContainer = boosterSOContainer;
        JarSOContainer = jarSOContainer;

        _onReady.Invoke(this);
    }

}
