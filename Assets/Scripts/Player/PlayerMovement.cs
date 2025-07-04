using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform cameraObject;
    private InputManager inputManager;
    private Vector3 moveDir;
    private Rigidbody body;
    private Transform playerTransform;
    private AnimationManager animationManager;
    private PlayerManager playerManager;

    public GameObject freeCamera;

    [Header("Movement")]
    [SerializeField] private float walkingSpeed = 3;
    [SerializeField] private float movementSpeed = 6;
    [SerializeField] private float sprintSpeed = 8;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float fallingSpeed = 50;

    [Header("Detections")]
    [SerializeField] float groundDetectionStartPoint = 0.5f;
    [SerializeField] float groundDetectionDistance = -0.2f;
    [SerializeField] float fallMinDistance = 1f;
    LayerMask groundMask;
    private float fallingTimer;

    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        cameraObject = Camera.main.transform;
        animationManager = GetComponentInChildren<AnimationManager>();
        animationManager.Initialize();
        playerManager.setIsGrounded(true);
        groundMask = ~(1 << 8 | 1 << 11); 
    }

    private void Start()
    {
        Initialize();
    }

    #region Move

    Vector3 movementVector;
    Vector3 direction;

    public void Move()
    {
        if (inputManager.getIsRolling() || playerManager.getIsInteracting()) return;


        moveDir = cameraObject.forward * inputManager.getVerticalValue();
        moveDir += cameraObject.right * inputManager.getHorizontalValue();
        moveDir.Normalize();

        

        if(inputManager.getIsSprinting() && inputManager.getMovementAmount() > 0.5f)
        { 
            moveDir = moveDir * sprintSpeed;
            playerManager.setIsSprinting(true); 

        }
        else
        {
            if (inputManager.getMovementAmount() < 0.5f)
            {
                moveDir = moveDir * walkingSpeed;
                playerManager.setIsSprinting(false);
            }
            else { moveDir = moveDir * movementSpeed; playerManager.setIsSprinting(false); }
        }

        Vector3 velocity = Vector3.ProjectOnPlane(new Vector3(moveDir.x, 0, moveDir.z), movementVector);

        body.linearVelocity = velocity;
        animationManager.UpdateValues(inputManager.getMovementAmount(), 0, playerManager.getIsSprinting());
    
    }

    public void Rotate()
    {
        if (animationManager.getCanRotate())
        {
            Vector3 rotateDir = Vector3.zero;
            direction = cameraObject.forward * inputManager.getVerticalValue();
            direction += cameraObject.right * inputManager.getHorizontalValue();
            direction.Normalize();
            direction.y = 0;

            if (direction == Vector3.zero)
            {
                direction = playerTransform.forward;
            }

            Quaternion rotationDir = Quaternion.LookRotation(direction);
            Quaternion rotation = Quaternion.Slerp(playerTransform.rotation, rotationDir, rotationSpeed * Time.deltaTime);


            playerTransform.rotation = rotation;
        }
    }

    public void Roll()
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

    public void Fall(Vector3 moveDirection)
    {
        
        playerManager.setIsGrounded(false);
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += groundDetectionStartPoint;
        if(Physics.Raycast(origin, transform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if(playerManager.getIsFalling())
        {
            body.AddForce(-Vector3.up * fallingSpeed);
            body.AddForce(moveDirection * fallingSpeed / 5f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDetectionDistance;
        direction = transform.position;

        Debug.DrawRay(origin, -Vector3.up * fallMinDistance, Color.red, 0.1f, false);
       
        if(Physics.Raycast(origin, -Vector3.up, out hit, fallMinDistance, groundMask))
        { 
            movementVector = hit.normal;
            Vector3 targetPosition = hit.point;
            playerManager.setIsGrounded(true);
            direction.y = targetPosition.y;

            if(playerManager.getIsFalling())
            {
                if(fallingTimer > 0.5f)
                { 
                    animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.land], true);
                } else
                {
                    animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.empty], false);
                    fallingTimer = 0;
                }
                playerManager.setIsFalling(false);
            } 

          
        }
        else
        {
            if (playerManager.getIsGrounded())
            {
                playerManager.setIsGrounded(false);
            }

            if (playerManager.getIsFalling() == false)
            {
                if (playerManager.getIsInteracting() == false)
                {
                    animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.falling], true);
                }

                Vector3 velocity = body.linearVelocity;

                velocity.Normalize();

                body.linearVelocity = velocity * (movementSpeed / 2);
                playerManager.setIsFalling(true);
            }
        }

        if (playerManager.getIsGrounded())
        {
            if (playerManager.getIsInteracting() || inputManager.getMovementAmount() > 0)
            {
                transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime);
            }
            else
            {
                transform.position = direction;
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

    public Vector3 getMovementDirection()
    {
        return moveDir;
    }

    public float getFallingTimer() 
    { 
        return fallingTimer;
    }

    #endregion

    #region Setters

    public void setFallingTimer(float timer)
    {
        fallingTimer = timer;
    }
    
    #endregion
}
