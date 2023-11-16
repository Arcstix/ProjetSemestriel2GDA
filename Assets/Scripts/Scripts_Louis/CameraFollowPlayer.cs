using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va permettre de Suivre le Player sans avoir cette rotation si c'�tait un enfant du player
public class CameraFollowPlayer : MonoBehaviour
{
    // Ce script attach� � la cam�ra � besoin d'une r�f au player (pas opti mais fera l'affaire pour l'instant)
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime; // Correspond � la fluidit� du suivie.

    private Vector3 _offset; // est la position de la cam�ra par rapport au player.
    private Vector3 _currentVelocity = Vector3.zero; // C'est une ref pour le SmoothDamp qu'on utilise.

    private void Awake()
    {
        if(_playerTransform == null)
        {
            Debug.LogError("le player n'est pas r�f�renc�.");
        }
        _offset = transform.position - _playerTransform.position; // Important de bien plac� la cam�ra dans la sc�ne.
    }

    // Les mouvements d'une cam�ra se font en LateUpdate
    private void LateUpdate()
    {
        // On traque la position du player tout le temps pour le suivre.
        Vector3 targetPosition = _playerTransform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
}
