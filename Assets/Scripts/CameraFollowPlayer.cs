using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime;

    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        if(_playerTransform == null)
        {
            Debug.LogError("le player n'est pas référencé.");
        }
        _offset = transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _playerTransform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
}
