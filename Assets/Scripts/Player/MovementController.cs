using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Range(4, 10)][SerializeField]public float movementSpeed = 4.0f;
    private float _startMovementSpeed;
    private Rigidbody rigidBody;

    private Quaternion Rotation;
    
    [SerializeField] private Camera Camera;
    
    private Vector3 mousePos;
    private Vector3 moveDir;
    
    private Ray ray;
    private RaycastHit raycstHit;

    private void OnEnable()
    {
        GameManager.Instance.PlayerCollect.OnTakeObject.AddListener(Slowdown);
        GameManager.Instance.PlayerCollect.OnDropObject.AddListener(RestoreSpeed);
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerCollect.OnTakeObject.RemoveListener(Slowdown);
        GameManager.Instance.PlayerCollect.OnDropObject.RemoveListener(RestoreSpeed);
    }

    private void Awake()
    {
        _startMovementSpeed = movementSpeed;
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 Direction)
    {
        moveDir = Direction*movementSpeed;
        moveDir.y = rigidBody.velocity.y;
        rigidBody.velocity = moveDir;
    }

    public void Rotate()
    {

        ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycstHit, float.MaxValue))
            mousePos = raycstHit.point;
        mousePos.y = transform.position.y;
        transform.LookAt(mousePos);
    }
    private void Slowdown(GameObject gameObject)
    {
        Slowdown(gameObject.GetComponent<CollectableObject>().Slowdown);
    }

    private void Slowdown(float value)
    {
        movementSpeed -= value;
    }

    private void RestoreSpeed(GameObject _) // Here argument in unnecessary, but required.
    {
        movementSpeed = _startMovementSpeed;
    }
}

