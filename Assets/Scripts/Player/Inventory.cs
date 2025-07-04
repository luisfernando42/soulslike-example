using UnityEngine;

public class Inventory : MonoBehaviour
{
    WeaponManager weaponManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    private void Awake()
    {
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    private void Start()
    {
        weaponManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponManager.LoadWeaponOnSlot(leftWeapon, true);
    }
}
