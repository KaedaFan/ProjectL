using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{   
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _player;

    [SerializeField] private Stats _stats;
    [SerializeField] private float _moveSpeedFromInspector = 5f;

    private Rigidbody2D _rigibody;

    private Vector2 _movementDirection;
    private Vector2 _movementInput;
    private float _movementSpeed;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        if (_player == null) _player = this.gameObject;

        //скорость передвижения загружается из SO скрипта если он создан
        if (_stats != null) _movementSpeed = _stats.MovementSpeed;
        else _movementSpeed = _moveSpeedFromInspector;
    }

    private void Update()
    {
        GetJoystickInput();
    }

    private void FixedUpdate()
    {
        MovePlayerr();
    }

    private void GetJoystickInput()
    {
        _movementInput = _joystick.InputDirection.normalized;
    }

    private void MovePlayerr()
    {
        _movementDirection = _rigibody.position + _movementInput * _movementSpeed * Time.fixedDeltaTime;
        _rigibody.MovePosition(_movementDirection);
    }
}
