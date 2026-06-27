using Common;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Microbe))]
    public class BossMicrobe : MonoBehaviour
    {
        private void OnDestroy()
        {
            this.Inject<AudioManager>().PlayNormalMusic();
        }
    }
}
