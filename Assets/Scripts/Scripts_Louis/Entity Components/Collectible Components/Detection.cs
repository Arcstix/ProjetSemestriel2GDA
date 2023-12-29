using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private CollectibleManager _collectibleManager;
    private CollectibleMovement _collectibleMovement;
    private List<Transform> _targetList = new List<Transform>();

    private Transform _closestTarget;

    public Transform ClosestTarget { get => _closestTarget; }

    private void Awake()
    {
        _collectibleMovement = GetComponent<CollectibleMovement>();
    }

    private void Update()
    {
        // On vérifie d'abord si le collectible possède une target sinon le reste n'a pas lieu d'être.
        if(_targetList == null) 
        {
            //_collectibleManager = GetComponentInParent<CollectibleManager>();
            //_collectibleManager.ReciveNoTarget(this.gameObject);
            return; 
        }

        // On vérifie si la target la plus proche stockée est toujours la target la plus proche.
        if(_closestTarget != CalculateClosestTarget())
        {
            // La target la plus proche a changé alors on l'informe au Manager et on change la variable qui correspond à la target la plus proche.
            _closestTarget = CalculateClosestTarget();
            SendTarget();
        }
    }

    private void SendTarget()
    {
        if (TryGetComponent<CollectibleManager>(out CollectibleManager collectibleManager))
        {
            _collectibleManager = collectibleManager;
            _collectibleManager.ReciveClosestTarget(_closestTarget, this.gameObject);
        }
        else
        {
            _collectibleMovement.DefineTarget(_closestTarget);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie que ce que l'on détecte est bien une entité (Player ou Mobs)
        if (other.CompareTag("Entity"))
        {
            Transform newTarget = other.GetComponent<Transform>();

            // Si le collectible n'a pas de cible alors le premier qu'il détecte est forcément le plus proche.
            if(_closestTarget == null)
            {
                _closestTarget = newTarget;
                SendTarget();
            }

            if (!_targetList.Contains(newTarget))
            {
                _targetList.Add(newTarget);
            }            
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Entity"))
    //    {           
    //        Transform target = other.GetComponent<Transform>();
    //        if (_targetList.Contains(target))
    //        {
    //            _targetList.Remove(target);
    //        }
    //    }
    //}

    private Transform CalculateClosestTarget()
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
}
