using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private Rigidbody _playerRb;
    private Vector3 _targetPosition;
    private Vector3 _directionVector;
    private bool _onClick = false;

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
        if (OnClick)
        {
            //On calcule le Vecteur direction entre le player et la position de la target et on le normalize
            _directionVector = (_targetPosition - transform.position).normalized;
            //Debug.Log("Direction du déplacement : " + _directionVector);

            //On calcule la distance entre la position du player et de la target pour la vitesse.
            float distance = (_targetPosition - transform.position).magnitude;
            //Debug.Log("Distance entre le Player et la souris : " + distance);

            //On applique la force au Player.
            _playerRb.velocity = _directionVector * distance * _speed * Time.fixedDeltaTime;
        }
        else
        {
            _playerRb.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector * 3);
    }
}
