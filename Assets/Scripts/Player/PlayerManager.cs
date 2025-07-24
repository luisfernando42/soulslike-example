using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;
    private bool isInteracting;
    private bool isSprinting = false;
    private bool isFalling;
    private bool isGrounded;
    private bool canCombo;
    private CameraManager cameraManager;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        cameraManager = FindAnyObjectByType<CameraManager>();
    }

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponentInChildren<PlayerMovement>();

    }


    void Update()
    {
        isInteracting = animator.GetBool(AnimationKeys.INTERACTING);
        canCombo = animator.GetBool(AnimationKeys.COMBO);

        inputManager.TickInput();
        playerMovement.Move();
        playerMovement.Rotate();
        playerMovement.Roll();
        playerMovement.Fall(playerMovement.getMovementDirection());
    }

    private void FixedUpdate()
    {
        HandleCamera();
    }

    private void LateUpdate()
    {
        inputManager.SetIsSprinting(false);
        inputManager.SetIsRolling(false);
        inputManager.ResetRollInput();
        inputManager.ResetLightAttackInput();
        inputManager.ResetHeavyAttackInput();

        HandleFall();
    }

    private void HandleCamera()
    {

        if (cameraManager != null)
        {
            cameraManager.FollowTarget();
            cameraManager.Rotation(inputManager.getMouseX(), inputManager.getMouseY());
        }
    }

    private void HandleFall()
    {
        if (isFalling)
        {
            playerMovement.setFallingTimer(playerMovement.getFallingTimer() + Time.deltaTime);
        }
    }

    #region Getters

    public bool getIsInteracting()
    {
        return isInteracting;
    }

    public bool getIsSprinting()
    {
        return isSprinting;
    }

    public bool getIsFalling()
    {
        return isFalling;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public bool getCanCombo()
    {
        return canCombo;
    }

    #endregion

    #region Setters
    public void setIsSprinting(bool value)
    {
        isSprinting = value;
    }

    public void setIsGrounded(bool value)
    {
        isGrounded = value;
    }

    public void setIsFalling(bool value)
    {
        isFalling = value;
    }
    #endregion
}
