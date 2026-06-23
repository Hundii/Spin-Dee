using UnityEngine;

namespace Core
{
    public class Jar : MonoBehaviour
    {
        [SerializeField] private LiquidSO liquidSO;

        public LiquidSO GetLiquidSO()
        {
            return liquidSO;
        }
    }
}
