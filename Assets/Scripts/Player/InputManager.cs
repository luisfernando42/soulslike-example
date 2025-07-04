using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class InputManager : MonoBehaviour
{

    private float horizontalValue;
    private float verticalValue;
    private float movementAmount;
    private float mouseX;
    private float mouseY;
    
    private bool rollInput;
    private bool lightAttackInput;
    private bool heavyAttackInput;

    private bool isRolling;
    private bool isSprinting;
    private float rollInputTimer;

    private PlayerInputActions actions;
    private PlayerAttacker attacker;
    private Inventory inventory;
    private Vector2 movementInput;
    private Vector2 cameraMovementInput;

    private void Awake()
    {
        attacker = GetComponent<PlayerAttacker>();
        inventory = GetComponent<Inventory>();
    }

    public void OnEnable()
    {
      if (actions == null) 
      {
        actions = new PlayerInputActions();
        actions.Movement.Move.performed += actions => movementInput = actions.ReadValue<Vector2>();
        actions.Movement.CameraMovement.performed += i => cameraMovementInput = i.ReadValue<Vector2>(); 
        
      }
        actions.Enable();
    }

    public void OnDisable()
    {
        actions.Disable();
    }

    public void TickInput()
    {

        HandleMovement();
        HandleRolling();
        HandleAttack();
    }

    public void HandleMovement()
    {
        horizontalValue = movementInput.x;
        verticalValue = movementInput.y;
        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontalValue) + Mathf.Abs(verticalValue));
        mouseX = cameraMovementInput.x;
        mouseY = cameraMovementInput.y;
    }



    private void HandleRolling()
    {
        rollInput = actions.Actions.Roll.IsPressed();

        if (rollInput)
        {
            rollInputTimer += Time.deltaTime;
            if(movementAmount > 0)
                SetIsSprinting(true);
        } else
        {
            if(rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                SetIsSprinting(false);
                SetIsRolling(true);
            }
            rollInputTimer = 0;
        }
    }

    private void HandleAttack()
    {
        lightAttackInput = actions.Actions.LightAttack.IsPressed();
        heavyAttackInput = actions.Actions.HeavyAttack.IsPressed();

        if (lightAttackInput)
        {
            attacker.HandleLightAttack(inventory.rightWeapon);
        }

        if (heavyAttackInput)
        {
            attacker.HandleHeavyAttack(inventory.rightWeapon);
        }
    }

    public void ResetRollInput()
    {
        rollInput = false; 
    }

    public void SetIsRolling(bool value)
    {
        isRolling = value;
    }

    public void SetIsSprinting(bool value)
    {
        isSprinting = value;
    }

    public void ResetLightAttackInput()
    {
        lightAttackInput = false;
    }
    public void ResetHeavyAttackInput()
    {
        heavyAttackInput = false;
    }

    #region Getters

    public bool getRollInput()
    {
        return rollInput;
    }

    public float getMovementAmount()
    {
        return movementAmount;
    }


    public float getVerticalValue()
    {
        return verticalValue;
    }

    public float getHorizontalValue()
    {
        return horizontalValue;
    }

    public bool getIsRolling()
    {
        return isRolling;
    }

    public bool getIsSprinting()
    {
       return isSprinting;
    }

    public float getMouseX()
    {
        return mouseX;
    }
    public float getMouseY()
    {
        return mouseY;
    }
    #endregion
}

