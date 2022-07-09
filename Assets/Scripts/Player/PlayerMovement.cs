using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    [SerializeField]
    float _acceleration = 10f;
    [SerializeField]
    float _braking = 10f;
    [SerializeField]
    float _moveInput;
    [SerializeField]
    float _currentSpeed;
    Rigidbody2D _rb;
    [SerializeField]
    float _speed = 100;
    [SerializeField]
    float _jumpForce = 50;
    bool _isOnTheFloor = true;
    float _horizontalMove;
    
    
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    bool _flippedToRight = true;
    private Interactables currentNpc;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        // _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _horizontalMove = _currentSpeed * _moveInput;
        _animator.SetFloat("jumpingVelocity", _rb.velocity.y);
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMove, _rb.velocity.y);
        _animator.SetFloat("speed", Mathf.Abs(_horizontalMove));
        _rb.velocity = new Vector2(_currentSpeed, _rb.velocity.y);

        if (_moveInput != 0.0f)
        {
            _currentSpeed += _acceleration * Time.deltaTime * Mathf.Sign(_moveInput);
            _currentSpeed = Mathf.Clamp(_currentSpeed, -_speed, _speed);
        }
        else
        {
            if (_currentSpeed > 0)
            {
                _currentSpeed -= _braking * Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _speed);
            }
            else if (_currentSpeed < 0)
            {
                _currentSpeed += _braking * Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, -_speed, 0);
            }
        }
    }

    void Flip()
    {
        if (_moveInput < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_moveInput > 0)
        {
            _spriteRenderer.flipX = false; 
        }
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            if(_isOnTheFloor) _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jumped");
        }
    }
    
    void OnInteract(InputValue value) => currentNpc?.OnInteract();

    Tween currentMoveTween;
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<float>();
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Platform")
        {
            _isOnTheFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            _isOnTheFloor = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out Interactables npc))
        {
            currentNpc = npc;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out Interactables npc))
        {
            currentNpc = null;
        }
    }   
}
