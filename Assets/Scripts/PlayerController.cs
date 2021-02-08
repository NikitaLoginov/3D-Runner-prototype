using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _playerAnimator;
    private Rigidbody _playerRb;
    
    //Jumping
    private bool _isOnGround;
    private bool _canJump;
    private float _jumpForce = 500f;
    private float gravityModifier = 1.5f;
    private float _movingSpeed = 15f;
    
    //Moving
    private bool _canMove;
    private Vector3 _left;
    private Vector3 _right;
    [SerializeField] private Transform _targetLeft;
    [SerializeField] private Transform _targetRight;
    [SerializeField] private Transform _targetCenter;
    private Transform _target;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerTransform = this.transform;
        _playerTransform.transform.position = _targetCenter.transform.position;
        
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetFloat("Speed_f", 3f); // setting speed value >3 to switch to running animation

        _target = _targetCenter;

        Physics.gravity *= gravityModifier; // creating more gravity
    }

    // Here I keep input and movement in separate update functions since movement is physics based
    private void Update()
    {
        CheckingInput();
    }

    private void FixedUpdate()
    {
        if (_canJump)
        {
            ApplyForce();
        }
        
        if(_canMove)
            HorizontalMovement();
    }

    private void CheckingInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _canJump = true;
        }
        // Checking for directional input
        // Then checking for player position
        // Then assigning target for HorizontalMovement script
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Math.Abs(_playerTransform.position.x -_targetCenter.position.x) < 0.001f) //do not comparing floats directly to avoid rounding
            {
                _target = _targetLeft;
            }

            else if (Math.Abs(_playerTransform.position.x -_targetRight.position.x) < 0.001f)
                _target = _targetCenter;
            
            _canMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(Math.Abs(_playerTransform.position.x - _targetCenter.position.x) < 0.001f)
                _target = _targetRight;
            else if (Math.Abs(_playerTransform.position.x - _targetLeft.position.x) < 0.001f)
                _target = _targetCenter;
            
            _canMove = true;
        }
    }

    //Method for horizontal movement
    private void HorizontalMovement()
    {
        float step = _movingSpeed * Time.deltaTime;
        _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _target.position, step);
        
        if(Vector3.Distance(_playerTransform.position, _target.position) < 0.001f)
        {
            _canMove = false;
        }
            
    }

    //Method for jumping
    private void ApplyForce()
    {
        _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isOnGround = false;
        _canJump = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            _isOnGround = true;
    }
}
