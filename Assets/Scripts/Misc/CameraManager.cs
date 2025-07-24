using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraObj;
    [SerializeField] private Transform pivotObj;

    private Vector3 position;
    private Vector3 cameraMovement = Vector3.zero;
    private LayerMask mask;


    private float rotationSpeed = 0.1f;
    private float movementSpeed = 0.1f;
    private float pivotSpeed = 0.03f;
    private float collisionRadius = 0.2f;
    private float collisionOffset = 0.2f;
    private float minCollisionOffset = 0.2f;
    private float targetZ;
    private float cameraZ;
    private float angle;
    private float pivotAngle;
    private float minPivot = -35;
    private float maxPivot = 35;
    public static CameraManager instance;





    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else return;
    }
    private void Awake()
    {
        Initialize();
        cameraZ = cameraObj.position.z;
        mask = ~(1 << 9 | 1 << 9 | 1 << 10);
        target = FindAnyObjectByType<PlayerManager>().transform;

    }

    public void FollowTarget()
    {
        
        Vector3 targetPos =  Vector3.SmoothDamp
            (transform.position, target.position, ref cameraMovement,Time.fixedDeltaTime / movementSpeed);
        transform.position = targetPos;
        HandleCollisions();

    }

    public void Rotation(float inputX, float inputY)
    {
        angle += (inputX * rotationSpeed) / Time.fixedDeltaTime;
        pivotAngle -= (inputY * pivotSpeed) / Time.fixedDeltaTime;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = angle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        
        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        pivotObj.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        targetZ = cameraZ;
        RaycastHit hit;
        Vector3 dir = cameraObj.position - pivotObj.position;
        dir.Normalize();

        if (Physics.SphereCast(pivotObj.position, collisionRadius, dir, out hit, Mathf.Abs(targetZ), mask))
        {
            float distance = Vector3.Distance(pivotObj.position, hit.point);
            targetZ = -(distance - collisionOffset);
        }
        if(Mathf.Abs(targetZ) < minCollisionOffset)
        {
            targetZ = -minCollisionOffset;
        }

        position.z = Mathf.Lerp(cameraObj.localPosition.z, targetZ, Time.fixedDeltaTime / collisionOffset);
        cameraObj.localPosition = position;
    }
}
