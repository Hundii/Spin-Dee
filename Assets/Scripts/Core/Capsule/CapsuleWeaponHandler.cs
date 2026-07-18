using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class CapsuleWeaponHandler : MonoBehaviour
    {
        private List<CapsuleWeapon> weapons;

        private SubscriptionList subscriptions = new();

        private void Start()
        {
            weapons = GetComponentsInChildren<CapsuleWeapon>().ToList();

            foreach (var weapon in weapons)
            {
                subscriptions.Add(weapon.WeaponHit.RegisterListener(new(HandleWeaponHit)));
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

        private void OnDestroy()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
