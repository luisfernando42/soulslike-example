using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject prefab;
    public bool isUnarmed;

    [Header("One Handed Attack Animations")]
    public string OneHandedLightAttack_1;
    public string OneHandedLightAttack_2;
    public string OneHandedHeavyAttack_1;
}
