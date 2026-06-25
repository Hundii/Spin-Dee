using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class CapsuleWeaponHandler : MonoBehaviour
    {
        private List<CapsuleWeapon> weapons;

        private void Start()
        {
            weapons = GetComponentsInChildren<CapsuleWeapon>().ToList();

            foreach (var weapon in weapons)
            {
                weapon.WeaponHit.RegisterListener(HandleWeaponHit);
            }
        }

        private void HandleWeaponHit(CapsuleWeapon capsuleWeapon)
        {
            foreach (var weapon in weapons)
            {
                if (weapon != capsuleWeapon)
                {
                    weapon.ResetCooldown();
                }
            }
        }
    }
}
