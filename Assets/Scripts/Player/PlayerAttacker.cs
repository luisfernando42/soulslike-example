using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimationManager animationManager;
    private InputManager inputManager;
    private string lastAttack;

    private void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
        inputManager = GetComponent<InputManager>();
    }

    public void HandleCombo(WeaponItem weapon)
    {
        if (inputManager.getIsMidCombo())
        {
            animationManager.setCanCombo(false);
            if (lastAttack == weapon.OneHandedLightAttack_1)
            {
                animationManager.playAnimation(weapon.OneHandedLightAttack_2, true);
            }
        }

    }
    public void HandleLightAttack(WeaponItem weapon)
    {
        animationManager.playAnimation(weapon.OneHandedLightAttack_1, true);
        lastAttack = weapon.OneHandedLightAttack_1;
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animationManager.playAnimation(weapon.OneHandedHeavyAttack_1, true);
        lastAttack = weapon.OneHandedHeavyAttack_1;
    }
}
