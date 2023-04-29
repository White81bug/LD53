using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Range(4, 10)][SerializeField]public float movementSpeed = 4.0f;
    private float _startMovementSpeed;
    private Rigidbody rigidBody;

    private Quaternion Rotation;
    [SerializeField] private float dashForce = 30;
    private bool _isDashing;
    [SerializeField] private float _dashTime = 0.5f; 
    [SerializeField] private float _dashSpeed = 10; 
    [SerializeField] private AnimationCurve  _dashSpeedCurve; 

    [SerializeField] private Camera Camera;
    
    private Vector3 mousePos;
    private Vector3 moveDir;
    
    private Ray ray;
    private RaycastHit raycstHit;

    private void OnEnable()
    {
        PlayerCollect.OnTakeObject.AddListener(Slowdown);
        PlayerCollect.OnDropObject += RestoreSpeed;
    }

    private void OnDisable()
    {
        PlayerCollect.OnTakeObject.RemoveListener(Slowdown);
        PlayerCollect.OnDropObject -= RestoreSpeed;
    }

    private void Awake()
    {
        _startMovementSpeed = movementSpeed;
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 Direction)
    {
        moveDir = Direction*movementSpeed;
        rigidBody.velocity = moveDir;
    }

    public void Stay()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public void Rotate()
    {

        ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycstHit, float.MaxValue))
            mousePos = raycstHit.point;
        mousePos.y = transform.position.y;
        transform.LookAt(mousePos);
    }
    public IEnumerator Dash()
    {
        Vector3 direction = transform.forward;
        if (rigidBody.velocity != Vector3.zero)
            direction = rigidBody.velocity.normalized;
        if (direction == Vector3.zero) yield break;
        if (_isDashing) yield break;

        _isDashing = true;

        var elapsedTime = 0f;
        while (elapsedTime < _dashTime)
        {
            var velocityMultiplier = _dashSpeed * _dashSpeedCurve.Evaluate(elapsedTime);

            ApplyVelocity(direction, velocityMultiplier);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _isDashing = false;
    }

    private void ApplyVelocity(Vector3 desiredVelocity, float multiplier) // Дублирующийся код всегда выносить в отдельный метод
    {
        var velocity = rigidBody.velocity;

        velocity.y = desiredVelocity.y == 0 ? velocity.y : desiredVelocity.y * multiplier;// чтобы не ломать физику, скорость по Y будет изменяться только если это нужно
        velocity.x = desiredVelocity.x * multiplier;
        velocity.z = desiredVelocity.z * multiplier;

        rigidBody.velocity = velocity;
    }

    private void Slowdown(float value)
    {
        Debug.Log($"slowdown {value}");
        movementSpeed -= value;
    }

    private void RestoreSpeed()
    {
        movementSpeed = _startMovementSpeed;
    }
}

