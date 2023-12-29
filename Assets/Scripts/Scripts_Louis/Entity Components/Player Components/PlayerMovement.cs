using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private Rigidbody _playerRb;
    private Vector3 _targetPosition;
    private Vector3 _directionVector;
    private bool _onClick = false;

    private Vector2 _moveInputValue;
    public float distance;

    public bool OnClick { get => _onClick; set => _onClick = value; } // On applique une force au player seulement pendant un clique.

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void FixedUpdate()
    {
        CalculateDirection();
        distance = CalculateDistanceToTarget();
        ApplyVelocity(distance);
    }

    /// <summary>
    /// On applique la force au Player.
    /// </summary>
    /// <param name="distance"></param>
    private void ApplyVelocity(float distance)
    {        
        _playerRb.velocity = _directionVector * distance * _speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// On calcule la distance entre la position du player et de la target pour la vitesse.
    /// </summary>
    /// <returns></returns>
    private float CalculateDistanceToTarget()
    {        
        return (_targetPosition - transform.position).magnitude;
        //Debug.Log("Distance entre le Player et la souris : " + distance);
    }

    /// <summary>
    /// On calcule le Vecteur direction entre le player et la position de la target et on le normalize
    /// </summary>
    private void CalculateDirection()
    {       
        _directionVector = (_targetPosition - transform.position).normalized;
        //Debug.Log("Direction du déplacement : " + _directionVector);
    }

    private void OnMove(InputValue value)
    {
        Vector2 Input = _moveInputValue;
        distance = 1;
        _directionVector = new Vector3(Input.x, 0, Input.y);
        Debug.Log("XBOX");
    }

    private void OnDrawGizmos()
    {
        // Direction du mouvement
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector * 3);
    }
}
