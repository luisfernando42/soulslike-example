using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform cameraObject;
    private InputManager inputManager;
    private Vector3 moveDir;
    private Rigidbody body;
    private Transform playerTransform;
    private AnimationManager animationManager;

    public GameObject freeCamera;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 6;
    [SerializeField] private float sprintSpeed = 8;
    [SerializeField] private float rotationSpeed = 10;
    private bool isSprinting = false;


    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        inputManager = GetComponent<InputManager>();
        cameraObject = Camera.main.transform;
        animationManager = GetComponentInChildren<AnimationManager>();
        animationManager.Initialize();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        isSprinting = inputManager.getRollInput();
        inputManager.TickInput();
        Move();

        if (animationManager.getCanRotate()) Rotate();
            Roll();

    }

    #region Move

    Vector3 movementVector;
    Vector3 direction;

    private void Move()
    {
        if (inputManager.getIsRolling()) return;

        moveDir = cameraObject.forward * inputManager.getVerticalValue();
        moveDir += cameraObject.right * inputManager.getHorizontalValue();
        moveDir.Normalize();

        

        if(inputManager.getIsSprinting())
        { 
            moveDir = moveDir * sprintSpeed;
            isSprinting = true; 

        }
        else moveDir = moveDir * movementSpeed;

        Vector3 velocity = Vector3.ProjectOnPlane(new Vector3(moveDir.x, 0, moveDir.z), movementVector);

        body.linearVelocity = velocity;
        animationManager.UpdateValues(inputManager.getMovementAmount(), 0, isSprinting);
    
    }

    private void Rotate()
    {
        Vector3 rotateDir = Vector3.zero;
        direction = cameraObject.forward * inputManager.getVerticalValue();
        direction += cameraObject.right * inputManager.getHorizontalValue();
        direction.Normalize();
        direction.y = 0;

        if(direction == Vector3.zero)
        {
            direction = playerTransform.forward;
        }

        Quaternion rotationDir = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Slerp(playerTransform.rotation, rotationDir, rotationSpeed * Time.deltaTime);

       
        playerTransform.rotation = rotation;
    }

    private void Roll()
    {
        if (animationManager.getIsInteracting()) return;

        
        if(inputManager.getIsRolling())
        {
            moveDir = cameraObject.forward * inputManager.getVerticalValue();
            moveDir += cameraObject.right * inputManager.getHorizontalValue();

            if (inputManager.getMovementAmount() > 0) 
            {
                animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.rolling], true);
                moveDir.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = rollRotation;
            } else
            {
                animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.backstep], true);
            }
        }
    }

    #endregion


    #region Getters
    public Transform getPlayerTransform()
    {
        return playerTransform;
    }

    public Rigidbody getPlayerRigidBody()
    {
        return body;
    }
    #endregion
}
