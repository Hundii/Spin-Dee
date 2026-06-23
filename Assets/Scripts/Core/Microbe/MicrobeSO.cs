using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Microbe/Definition")]
    public class MicrobeSO : ContainedSO
    {
        public GameObject prefab;
    }
}
