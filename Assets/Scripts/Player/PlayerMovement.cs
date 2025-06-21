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
    [SerializeField] private float rotationSpeed = 10;


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
        inputManager.TickInput();
        Move();
        if (animationManager.getCanRotate()) Rotate();

    }

    #region Move

    Vector3 movementVector;
    Vector3 direction;

    private void Move()
    {
        moveDir = cameraObject.forward * inputManager.getVerticalValue();
        moveDir += cameraObject.right * inputManager.getHorizontalValue();
        moveDir.Normalize();

        moveDir = moveDir * movementSpeed;

        Vector3 velocity = Vector3.ProjectOnPlane(new Vector3(moveDir.x, 0, moveDir.z), movementVector);

        body.linearVelocity = velocity;
        animationManager.UpdateValues(inputManager.getMovementAmount(), 0);
    
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

    #endregion


    #region Getters
    public Transform getPlayerTransform()
    {
        return playerTransform;
    }
    #endregion
}
