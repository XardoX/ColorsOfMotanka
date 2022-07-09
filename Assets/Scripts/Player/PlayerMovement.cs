using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
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
    }
    
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMove, _rb.velocity.y);
        _animator.SetFloat("speed", Mathf.Abs( _horizontalMove));
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
        if(_isOnTheFloor) _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetBool("jump",true);
    }
    
    void OnInteract(InputValue value) => currentNpc?.OnInteract();

    Tween currentMoveTween;
    void OnMove(InputValue value)
    {
        if(currentMoveTween != null) currentMoveTween.Kill();
        _moveInput = value.Get<float>();
        if(value.isPressed || _moveInput !=0f)
        {
            currentMoveTween =  DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _speed, 1);
        }
        else if(_moveInput == 0f)
        {
            currentMoveTween =  DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0, 1).SetEase(Ease.OutCirc);
        }
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Platform")
        {
            _isOnTheFloor = true;
            _animator.SetBool("jump", false);
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
