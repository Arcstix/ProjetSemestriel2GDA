using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollectibleMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private Transform _currentTarget;
    private List<Transform> _targetList = new List<Transform>();
    private Rigidbody _collectibleRb;
    private Vector3 _directionVector;

    private void Awake()
    {
        _collectibleRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Dans le cas ou le comestible à plusieurs target il doit avoir comme target celle qui se trouve le plus proche de lui
        if(_targetList.Count > 1)
        {
            _currentTarget = CompareDistance();
        }
    }

    private Transform CompareDistance()
    {
        // On initilise la première variable comme infini pour que la 1ère target de la liste l'a remplace
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        // On passe en revu chaque target dans la liste et on définit celle qui est la plus proche.
        foreach (var target in _targetList)
        {
            float distanceTarget = (target.position - transform.position).magnitude;

            if (distanceTarget < closestDistance)
            {
                closestDistance = distanceTarget;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private void FixedUpdate()
    {
        if (_currentTarget != null)
        {
            //On calcule le Vecteur direction entre le collectible et la position de la target et on le normalize
            _directionVector = (_currentTarget.position - transform.position).normalized;

            //On calcule la distance entre la position du collectible et de la target pour la vitesse.
            float distance = (_currentTarget.position - transform.position).magnitude;

            //On applique la force au Collectible.
            _collectibleRb.velocity = _directionVector * distance * _speed * Time.fixedDeltaTime;
        }
        else
        {
            _collectibleRb.velocity = Vector3.zero;
        }
    }


    /// <summary>
    /// Cette méthode permet d'ajouter une cible potentiel à suivre pour le comestible.
    /// </summary>
    /// <param name="newTarget"></param>
    public void AddPotentialTarget(Transform newTarget)
    {
        // On ajoute une target dans la liste des entitées détectées par le comestible.
        _targetList.Add(newTarget);

        // Dans le cas ou le comestible n'a qu'une target on lui assigne comme sa target actuel.
        if (_currentTarget == null)
        {           
            _currentTarget = newTarget;
            return;
        }        
    }

    /// <summary>
    /// Cette méthode permet de supprimer une cible potentiel de la liste.
    /// </summary>
    /// <param name="target"></param>
    public void RemovePotentialTarget(Transform target)
    {
        if (_targetList.Contains(target))
        {
            _targetList.Remove(target);
        }       
    }

    private void OnDrawGizmos()
    {
        // Direction du mouvement
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector * 3);
    }
}
