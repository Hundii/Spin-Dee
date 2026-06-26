using Common;
using UnityEngine;

namespace Core
{
    public class Jar : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshRenderer liquidMeshRenderer;
        
        private JarSO jarSO;
        private LiquidSO liquidSO;

        private void Awake()
        {
            var gameSelectionSceneData = this.Inject<SharedDataService>().GetData<GameSelectionSceneData>(false);
            jarSO = gameSelectionSceneData == null ? SOContainerContainer.JarSOContainer.turkoiseJar : gameSelectionSceneData.jarSO;
            liquidSO = gameSelectionSceneData == null ? SOContainerContainer.LiquidSOContainer.glitchedLiquid : gameSelectionSceneData.liquidSO;

            meshRenderer.material = jarSO.jarMaterial;
            liquidMeshRenderer.material = liquidSO.liquidMaterial;
        }

        public LiquidSO GetLiquidSO()
        {
            return liquidSO;
        }

        public JarSO GetJarSO()
        {
            return jarSO;
        }
    }
}
