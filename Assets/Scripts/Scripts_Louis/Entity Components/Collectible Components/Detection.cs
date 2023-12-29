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
        // On v�rifie d'abord si le collectible poss�de une target sinon le reste n'a pas lieu d'�tre.
        if(_targetList == null) 
        {
            //_collectibleManager = GetComponentInParent<CollectibleManager>();
            //_collectibleManager.ReciveNoTarget(this.gameObject);
            return; 
        }

        // On v�rifie si la target la plus proche stock�e est toujours la target la plus proche.
        if(_closestTarget != CalculateClosestTarget())
        {
            // La target la plus proche a chang� alors on l'informe au Manager et on change la variable qui correspond � la target la plus proche.
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
        // On v�rifie que ce que l'on d�tecte est bien une entit� (Player ou Mobs)
        if (other.CompareTag("Entity"))
        {
            Transform newTarget = other.GetComponent<Transform>();

            // Si le collectible n'a pas de cible alors le premier qu'il d�tecte est forc�ment le plus proche.
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
        // On initilise la premi�re variable comme infini pour que la 1�re target de la liste l'a remplace
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        // On passe en revu chaque target dans la liste et on d�finit celle qui est la plus proche.
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
