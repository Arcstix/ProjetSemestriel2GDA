using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Un facteur qui permet de remplacer la distance avec le clique")]
    [SerializeField] private float _joystickSpeedMultiplier = 40f;

    [SerializeField] private float _speed = 20f;

    private Rigidbody _playerRb;
    private Vector3 _targetPosition;
    private Vector3 _directionVector;
    private bool _onClick = false;

    private Vector3 _joystickDirection;
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
        // La partie de calcul de Velocité lors d'un click de souris car pas de direction de joystick.
        if (_joystickDirection == Vector3.zero)
        {  
            CalculateDirection();
            distance = CalculateDistanceToTarget();
            ApplyMouseVelocity(distance);
        }
        else
        {
            VelocityWithJoystick();
            _targetPosition = transform.position;
        }
        
    }

    /// <summary>
    /// On applique la force au Player.
    /// </summary>
    /// <param name="distance"></param>
    private void ApplyMouseVelocity(float distance)
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

    public void SetJoystickDirection(Vector3 joystickDirection)
    {
        _joystickDirection = joystickDirection;
    }


    // Appelé par InputReader quand un joystick est utilisé pour appliqué une vélocité.
    public void VelocityWithJoystick()
    {
        if(_playerRb.velocity.magnitude < _joystickSpeedMultiplier * _speed)
        {
            _playerRb.velocity = _joystickDirection * _joystickSpeedMultiplier * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        // Direction du mouvement
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector * 3);
    }
}
