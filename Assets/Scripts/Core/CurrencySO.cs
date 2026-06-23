using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName= Utility.DefaultScriptableObjectPrefix + "Currency")]
    public class CurrencySO : ItemDefinitionSO
    {
        public string currencyName;
    }
}
