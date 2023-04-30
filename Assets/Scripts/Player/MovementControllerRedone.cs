using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControllerRedone : MonoBehaviour
{
    public InputActions InputActions;
    public bool CanMove;

    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Transform _camera;
    private Vector3 _velocity;
    private Rigidbody _rb;
    private CameraController _cameraController;
    
    private PlayerAnimations _playerAnimations;
    

    private void Awake()
    {
        CanMove = true;
        
        _rb = GetComponent<Rigidbody>();

        InputActions = new InputActions();
        InputActions.Player.Enable();
        
        if (Camera.main != null) _camera = Camera.main.transform;
        _cameraController = FindObjectOfType<CameraController>();
        
        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void FixedUpdate()
    {

        //Reading controls & moving player
        Vector2 inputVector = InputActions.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        //Debug.Log($"Magnitude is {direction.magnitude}");


        if (direction.magnitude >= 0.1f & CanMove)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            _rb.MovePosition(this.transform.position + moveDir * (speed * Time.fixedDeltaTime));
            _playerAnimations.WalkingAnimation(true);
        }
        else
        {
            //_rb.MovePosition(Vector3.zero);
            _playerAnimations.WalkingAnimation(false);
        }
        #region CameraCantroller

        if (InputActions.FindAction("RightButton").WasPressedThisFrame())
            _cameraController.previousPosition =
                _cameraController.cam.ScreenToViewportPoint(Input.mousePosition);
        else if (InputActions.FindAction("RightButton").IsInProgress())
            _cameraController.Rotate();

        _cameraController.ChangeDistance(Input.mouseScrollDelta.y);
        _cameraController.moveToTarget();

        #endregion
    }
}
