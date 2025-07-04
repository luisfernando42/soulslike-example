using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimationManager animationManager;

    private void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
    }
    public void HandleLightAttack(WeaponItem weapon)
    {
        animationManager.playAnimation(weapon.OneHandedLightAttack_1, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animationManager.playAnimation(weapon.OneHandedHeavyAttack_1, true);
    }
}
