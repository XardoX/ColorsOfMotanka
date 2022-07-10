using System;
using System.Collections.Generic;
using DG.Tweening;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;
using MyBox;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Foldout("settings", true)] 
    float _acceleration = 10f;
    [SerializeField]
    float _braking = 10f;
    [SerializeField]
    float _moveInput;
    [SerializeField]
    float _speed = 100;
    [SerializeField]
    float _jumpForce = 50;
    [SerializeField][ReadOnly][Foldout("Ground Check", true)]
    float _groundCheckDistance;
    [SerializeField]
    LayerMask _groundCheckMask;
    [SerializeField][Foldout("References", true)] 
    SpriteRenderer _spriteRenderer;

    [SerializeField][ReadOnly][Foldout("Debug", true)] 
    float _currentSpeed;
    [SerializeField][ReadOnly]
    bool _isGrounded = false;
    Animator _animator;
    Rigidbody2D _rb;
    float _horizontalMove;

    public int coinAmount;
    
    bool _flippedToRight = true;
    private Interactables currentNpc;

    private GameObject afterEffect;
    
    private AudioSource audioSource;
    
    private Dictionary<string, AudioClip> audioLibrary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (var audioClip in Resources.LoadAll<AudioClip>("Music/"))
        {
            audioLibrary.Add(audioClip.name, audioClip);
        }
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _horizontalMove = _currentSpeed * _moveInput;
        _animator.SetFloat("speed", Mathf.Abs(_horizontalMove));
        GroundCheck();
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMove, _rb.velocity.y);
        _animator.SetFloat("jumpingVelocity", _rb.velocity.y);

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

        _rb.velocity = new Vector2(_currentSpeed, _rb.velocity.y);
    }
    void GroundCheck()
    {
        if(Physics2D.CircleCast(transform.position,0.25f, Vector3.down, _groundCheckDistance,_groundCheckMask))
        {
            if (!_isGrounded)
            {
                PlayAudioClip("Falling");
                
            }
            _isGrounded = true;
            Debug.DrawRay(transform.position, Vector3.down *_groundCheckDistance,Color.green);
        } else 
        {
            _isGrounded = false;
        }

        _animator.SetBool("IsGrounded", _isGrounded);

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
            if(_isGrounded)
            {
                PlayAudioClip("Jumping");
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _animator.SetTrigger("Jumped");
            }     
        }
    }
    
    void OnInteract(InputValue value) => currentNpc?.OnInteract();

    Tween currentMoveTween;
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<float>();
        Flip();
    }

    private void PlayAudioClip(string name)
    {
        audioSource.clip = audioLibrary[name];
        audioSource.Play();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
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
