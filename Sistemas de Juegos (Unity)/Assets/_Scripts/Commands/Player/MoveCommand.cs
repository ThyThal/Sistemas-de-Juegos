using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _direction;
    private float _speed;

    public MoveCommand(Rigidbody rigidbody, Transform transform, Vector3 direction, float speed)
    {
        _rigidbody = rigidbody;
        _transform = transform;
        _direction = direction;
        _speed = speed;
    }



    public void Do()
    {
        var movement = _direction * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_transform.position + movement);
    }
}
