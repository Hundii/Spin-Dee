using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/LootDrop")]
    public class LootDrop : ContainedSO
    {
        public Sprite icon;
        public virtual void Activate(float amount) { }
    }
}
