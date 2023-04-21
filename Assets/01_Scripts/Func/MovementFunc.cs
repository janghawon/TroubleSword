using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunc : MonoBehaviour
{
    [Header("컴포넌트")]
    private CharacterController _playerController;

    [Header("수치 조정")]
    public bool canMove;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _gravtiy;
    [SerializeField] private float _turnningSpeed;

    [Header("움직임 기능")]
    private Vector3 _moveDir;
    private float _verticalVelocity;

    public void SetDirection(Vector3 value)
    {
        _moveDir = value;
    }

    private void Awake()
    {
        canMove = true;
        _playerController = GetComponent<CharacterController>();
    }
    private void PlayerGravity()
    {
        if (_playerController.isGrounded == false)
        {
            _verticalVelocity = _gravtiy * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = _gravtiy * 0.3f * Time.fixedDeltaTime;
        }
    }
    private void CalculatorMove()
    {
        _moveDir *= _playerSpeed * Time.fixedDeltaTime;
        if (_moveDir.sqrMagnitude > 0 && canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(_moveDir),
                                                  Time.fixedDeltaTime * _turnningSpeed);

            Vector3 move = _moveDir + _verticalVelocity * Vector3.up;
            _playerController.Move(move);
        }
    }
    
    void FixedUpdate()
    {
        PlayerGravity();
        CalculatorMove();
    }
}
