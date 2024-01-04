using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerGround;

    private PlayerMovement _playerMovement;
    private Vector3 _joystickDirection;

    public Vector3 JoystickDirection { get => _joystickDirection; set => _joystickDirection = value; }

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

        // On récupère les valeurs du joystick et on en fait un Vecteur direction qu'on envoie au PlayerMovement.
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        _joystickDirection = new Vector3(_horizontal, 0, _vertical).normalized;
        _playerMovement.SetJoystickDirection(_joystickDirection);
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
