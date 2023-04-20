using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunc : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 0f, _gravity = -9.8f;

    private CharacterController _charController;
    private FuncAnimator _funcAnimator;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;
    private Vector3 _inputVelocity;

    public bool IsActiveMove { get; set; }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _funcAnimator = transform.Find("Visual").GetComponent<FuncAnimator>();
    }

    public void SetMovementVelocity(Vector3 value)
    {
        _inputVelocity = value;
        _movementVelocity = value;
    }

    private void CalculatePlayerMovement()
    {
        //����� ���߿�
        _inputVelocity.Normalize();

        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _inputVelocity;
        //���ʹϾ� ���� ��ȯ��Ģ ���� ���Ѵ�.

        //_funcAnimator?.SetSpeed(_movementVelocity.sqrMagnitude); //�̵��ӵ� �ݿ�

        _movementVelocity *= _moveSpeed * Time.fixedDeltaTime;
        if (_movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }

    }

    public void SetRotation(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
       // _funcAnimator?.SetSpeed(0); //�̵��ӵ� �ݿ�
    }

    private void FixedUpdate()
    {
        if (IsActiveMove)
        {
            CalculatePlayerMovement();
        }

        if (_charController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _charController.Move(move);
        //_funcAnimator?.SetAirbone(!_charController.isGrounded);
    }
}
