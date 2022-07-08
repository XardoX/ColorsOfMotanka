using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_currentSpeed * _moveInput, _rb.velocity.y);
    }

    void OnJump(InputValue value)
    {
        if(_isOnTheFloor) _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
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
}
