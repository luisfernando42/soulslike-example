using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    WeaponHolder leftHandHolder;
    WeaponHolder rightHandHolder;

    private void Awake()
    {
        WeaponHolder[] weaponSlots = GetComponentsInChildren<WeaponHolder>();
        foreach (WeaponHolder slot in weaponSlots)
        {
            if (slot.isOnLeftHand)
            {
                leftHandHolder = slot;
            } else rightHandHolder = slot;
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeftHanded)
    {
        if (isLeftHanded) leftHandHolder.LoadWeapon(weaponItem);
        else rightHandHolder.LoadWeapon(weaponItem);
    }
}
