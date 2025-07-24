using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    WeaponHolder leftHandHolder;
    WeaponHolder rightHandHolder;

    CollisionDamage leftHandCollisionDamage;
    CollisionDamage rightHandCollisionDamage;

    private void Awake()
    {
        WeaponHolder[] weaponSlots = GetComponentsInChildren<WeaponHolder>();
        foreach (WeaponHolder slot in weaponSlots)
        {
            if (slot.isOnLeftHand)
            {
                leftHandHolder = slot;
            }
            else rightHandHolder = slot;
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeftHanded)
    {
        if (isLeftHanded) { leftHandHolder.LoadWeapon(weaponItem); LoadLeftWeaponCollider(); }
        else { rightHandHolder.LoadWeapon(weaponItem); LoadRightWeaponCollider(); }
    }
    #region Handle Colliders
    private void LoadLeftWeaponCollider()
    {
        leftHandCollisionDamage = leftHandHolder.currentWeapon.GetComponentInChildren<CollisionDamage>();
    }

    private void LoadRightWeaponCollider()
    {
        rightHandCollisionDamage = rightHandHolder.currentWeapon.GetComponentInChildren<CollisionDamage>();
    }

    private void EnableRightHandCollider()
    {
        rightHandCollisionDamage.EnableCollider();
    }

    private void EnableLeftHandCollider()
    {
        leftHandCollisionDamage.EnableCollider();
    }

    private void DisableRightHandCollider()
    {
        rightHandCollisionDamage.DisableCollider();
    }

    private void DisableLeftHandCollider()
    {
        leftHandCollisionDamage.DisableCollider();
    }
    #endregion
}
