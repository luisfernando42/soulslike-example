using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform parentObj;
    public bool isOnLeftHand;
    public bool isOnRightHand;
    private GameObject currentWeapon;

    public void LoadWeapon(WeaponItem weapon)
    {
        DestroyCurrentWeapon();

        if (weapon == null)
        {
            UnloadWeapon();
            return;
        }

        GameObject weaponModel = Instantiate(weapon.prefab);
        if (weaponModel != null) 
        { 
            if(parentObj != null)
            {
                weaponModel.transform.parent = parentObj;
            } else
            {
                weaponModel.transform.parent = transform;
            }

            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
        }
        currentWeapon = weaponModel;
    }

    private void UnloadWeapon()
    {
        if (currentWeapon != null)
        { 
            currentWeapon.SetActive(false);
        }
    }

    private void DestroyCurrentWeapon()
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
    }
}
