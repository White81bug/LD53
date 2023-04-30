using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private MovementController _movementController;
    private CameraController _cameraController;

    private PlayerAnimations _playerAnimations;

    private PlayerInput _input;
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _movementController = GetComponent<MovementController>();
        _cameraController = FindObjectOfType<CameraController>();

        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        Vector2 moveVec = _input.actions.FindAction("Move").ReadValue<Vector2>();
        #region PlayerMovement

        if (moveVec != Vector2.zero)
        {
            var moveDir = new Vector3(moveVec.x, 0.0f, moveVec.y);
            var forward = _cameraController.transform.forward;
            var right = _cameraController.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            var desiredMoveDirection = forward * moveDir.z + right * moveDir.x;

            _movementController.Move(desiredMoveDirection);
            _playerAnimations.WalkingAnimation(true);
            Debug.Log("Not Zero");
        }
        else
        {
            Debug.Log("Zero");
            _movementController.Move(Vector3.zero);
            _playerAnimations.WalkingAnimation(false);
        }

        if (_input.actions.FindAction("Dash").WasPressedThisFrame())
            _movementController.StartCoroutine("Dash");

        _movementController.Rotate();

        #endregion

        #region CameraCantroller

        if (_input.actions.FindAction("RightButton").WasPressedThisFrame())
            _cameraController.previousPosition =
                _cameraController.cam.ScreenToViewportPoint(Input.mousePosition);
        else if (_input.actions.FindAction("RightButton").IsInProgress())
            _cameraController.Rotate();

        _cameraController.ChangeDistance(Input.mouseScrollDelta.y);
        _cameraController.moveToTarget();

        #endregion
    }
}