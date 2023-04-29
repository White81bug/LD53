using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private MovementController _movementController;
    private CameraController _cameraController;

    private PlayerInput _input;
    void Awake()
    {
        _input = FindObjectOfType<PlayerInput>();
        _movementController = FindObjectOfType<MovementController>();
        _cameraController = FindObjectOfType<CameraController>();
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
        }
        else
            _movementController.Stay();


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