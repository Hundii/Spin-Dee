using Common;
using System.Linq;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Item Definition/Basic")]
    public class ItemDefinitionSO : ContainedSO
    {
        public string id;
        public Sprite sprite;

        public static explicit operator ItemDefinitionSO(string id) =>
            SOContainerContainer.ItemDefinitionSOContainer.itemDefinitionSOArray.FirstOrDefault(x => x.id == id);
    }
}
