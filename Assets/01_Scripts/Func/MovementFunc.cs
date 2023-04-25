using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunc : MonoBehaviour
{
    [Header("������Ʈ")]
    private CharacterController _playerController;
    private FuncAnimator _funcAnimator;

    [Header("��ġ ����")]
    public bool canMove;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _gravtiy;
    [SerializeField] private float _turnningSpeed;

    [Header("������ ���")]
    public Vector3 MoveDir;
    private float _verticalVelocity;

    public void SetDirection(Vector3 value)
    {
        if(canMove)
        {
            MoveDir = value;
            _funcAnimator.SetMoveAnim(value);
        }
    }

    private void Awake()
    {
        canMove = true;
        _playerController = GetComponent<CharacterController>();
        _funcAnimator = GetComponent<FuncAnimator>();
    }

    public void StopImmediately()
    {
        MoveDir = Vector3.zero;
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
        MoveDir *= _playerSpeed * Time.fixedDeltaTime;
        if (MoveDir.sqrMagnitude > 0 && canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(MoveDir),
                                                  Time.fixedDeltaTime * _turnningSpeed);

            Vector3 move = MoveDir + _verticalVelocity * Vector3.up;
            _playerController.Move(move);
        }
    }
    
    void FixedUpdate()
    {
        PlayerGravity();
        CalculatorMove();
    }
}
