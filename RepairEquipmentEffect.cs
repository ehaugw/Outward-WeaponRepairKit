using UnityEngine;

namespace WeaponRepairKit
{
    class RepairEquipmentEffect : Effect
    {
        private const float REPAIR_AMOUNT = 50f;
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if (_affectedCharacter.CurrentWeapon is Weapon weapon)
            {
                weapon.SetDurabilityRatio(Mathf.Clamp((weapon.CurrentDurability + REPAIR_AMOUNT) / weapon.MaxDurability, 0f, 1));
            }
        }
    }
}
