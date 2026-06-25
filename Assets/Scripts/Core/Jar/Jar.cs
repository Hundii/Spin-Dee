using UnityEngine;

namespace Core
{
    public class Jar : MonoBehaviour
    {
        [SerializeField] private JarSO jarSO;
        [SerializeField] private LiquidSO liquidSO;

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
