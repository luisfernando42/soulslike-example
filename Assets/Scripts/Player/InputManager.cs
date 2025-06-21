using UnityEngine;


public class InputManager : MonoBehaviour
{

    private float horizontalValue;
    private float verticalValue;
    public float movementAmount;
    private float mouseX;
    private float mouseY;

    private PlayerInputActions actions;
    private CameraManager cameraManager;
    private Vector2 movementInput;
    private Vector2 cameraMovementInput;

    private void Awake()
    {
        cameraManager = CameraManager.instance;
    }

    private void FixedUpdate()
    {
        HandleCamera();
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
        MovementInput();
    }

    public void MovementInput()
    {
        horizontalValue = movementInput.x;
        verticalValue = movementInput.y;
        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontalValue) + Mathf.Abs(verticalValue));
        mouseX = cameraMovementInput.x;
        mouseY = cameraMovementInput.y;
    }

    private void HandleCamera()
    {
       
        if (cameraManager != null)
        {
            cameraManager.FollowTarget();
            cameraManager.Rotation(mouseX, mouseY);
        }
    }

    #region Getters

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
    #endregion
}

