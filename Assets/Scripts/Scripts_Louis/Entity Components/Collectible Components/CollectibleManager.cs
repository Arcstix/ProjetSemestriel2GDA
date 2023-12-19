using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va permettre de gérer toutes les targets de chaque collectibles et de réagir en conséquence.
public class CollectibleManager : MonoBehaviour
{
    private Transform _principleTarget;
    private CollectibleMovement _collectibleMovement;
    private CollectibleContainer _collectibleContainer;


    private void Awake()
    {
        _collectibleMovement = GetComponent<CollectibleMovement>();
        _collectibleContainer = GetComponent<CollectibleContainer>();
    }

    //public void ReciveNoTarget(GameObject refCollectible)
    //{
    //    _collectibleContainer.Eject(refCollectible);
    //}


    public void ReciveClosestTarget(Transform closestTarget, GameObject refCollectible)
    {
        // On initialise la target principale du Manager qui ne va pas changer et on définit la target pour le mouvement.
        if(_principleTarget == null)
        {
            _principleTarget = closestTarget;
            _collectibleMovement.DefineTarget(_principleTarget);
        }

        if(_principleTarget != closestTarget)
        {
            _collectibleContainer.Eject(closestTarget, refCollectible);
        }
    }
}
