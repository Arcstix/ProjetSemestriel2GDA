using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerGround;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Si le joueur appuie sur la touche gauche de sa souris alors on passe en "Mode clique"
        if (Input.GetMouseButton(0))
        {
            _playerMovement.OnClick = true;
        }
        else
        {
            _playerMovement.OnClick = false;
        }
    }

    private void FixedUpdate()
    {
        // On envoie la position de la souris au PlayerMovement lorsque le player clique.
        if (_playerMovement.OnClick)
        {
            Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, Mathf.Infinity, _layerGround))
            {
                _playerMovement.SetTargetPosition(hitInfo.point);
            }
        }
    }
}
