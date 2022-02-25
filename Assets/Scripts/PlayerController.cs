using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerCamera;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _moveSmoothTime = 0.2f;

    [Header("Mouse Look Settings")]
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _mouseSmoothTime = 0.02f;

    [Header("Fly settings")]
    [SerializeField] private float _flyStrength = 1f;

    private CharacterController _characterController;
    private float _cameraPitch = 0f;
    private bool _spaceBeingHeld = false;
    private bool _shiftBeingHeld = false;

    private Vector3 flyVelocity;
    private Vector3 velocity;
    private Vector2 currentDirection;
    private Vector2 currentDirectionVelocity;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _characterController.detectCollisions = false;
        LockAndHideCursor();
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        CheckForFlyKey();
    }

    private void CheckForFlyKey()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            _spaceBeingHeld = true;
            _shiftBeingHeld = false;
            return;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            _shiftBeingHeld = true;
            _spaceBeingHeld = false;
            return;
        }

        _spaceBeingHeld = false;
        _shiftBeingHeld = false;
    }

    private void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, _mouseSmoothTime);

        _cameraPitch -= currentMouseDelta.y * _mouseSensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);

        _playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * _mouseSensitivity);
    }

    private void UpdateMovement()
    {   
        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDirection.Normalize();

        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, _moveSmoothTime);

        if(_shiftBeingHeld && !_spaceBeingHeld)
        {
            flyVelocity = Vector3.down * _flyStrength;
            velocity = ((transform.forward * currentDirection.y + transform.right * currentDirection.x) * _moveSpeed) + flyVelocity;
        }
        if(_spaceBeingHeld && !_shiftBeingHeld)
        {
            flyVelocity = Vector3.up * _flyStrength;
            velocity = ((transform.forward * currentDirection.y + transform.right * currentDirection.x) * _moveSpeed) + flyVelocity;
        }
        if(!_spaceBeingHeld && !_shiftBeingHeld)
        {

            velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * _moveSpeed;
        }

        _characterController.Move(velocity * Time.deltaTime);
    }

    private void LockAndHideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
