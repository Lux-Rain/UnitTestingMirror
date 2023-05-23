using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private InputReader _inputReader;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnStartLocalPlayer()
    {
        _inputReader = new InputReader();
        ListenInput();
    }

    private void ListenInput()
    {
        _inputReader.OnMoveEvent += OnMove;
    }
    
    private void RemoveInput()
    {
        _inputReader.OnMoveEvent -= OnMove;
    }

    public void OnMove(Vector2 obj)
    {
        _moveInput = obj;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = (Vector3.forward * _moveInput.y + Vector3.right * _moveInput.x) * speed + Vector3.up * _rigidbody.velocity.y;
    }

    private void OnDestroy()
    {
        RemoveInput();
    }
}
