using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script attaché au Player va gérer le spawn des entités derrière le Player
public class SpawnChild : MonoBehaviour
{
    // Une liste de Transform pour le spawn d'entité.
    [SerializeField] private List<Transform> _spawnList = new List<Transform>();
    [SerializeField] private ChildLifeTime _childPrefab;

    // Cette liste va permettre de savoir les spawner qui sont utilisés.
    private List<Transform> _spawnUsed = new List<Transform>();


    /// <summary>
    /// Spawn une entité dans l'un des spawns libres du Player.
    /// </summary>
    public void SpawnChildEntity()
    {
        foreach (var spawn in _spawnList)
        {
            // On vérifie si le spawn est utilisé.
            if (!_spawnUsed.Contains(spawn))
            {
                _spawnUsed.Add(spawn); // On ajoute le spawn qui va être utilisé à la liste des spawn utilisé.
                ChildLifeTime newChild = Instantiate(_childPrefab, spawn);
                newChild.BeginLifeTime(this, spawn); // Une fois la création de l'entité on commence la durée de vie de l'entité.
                return; // On quitte la méthode plus tôt car la méthode a réalisé ce qu'on veut.
            }
        }
    }

    /// <summary>
    /// Permet de retirer de la liste des spawns utilisés les entités qui disparaissent.
    /// </summary>
    /// <param name="_refLocation"></param>
    public void ClearLocation(Transform _refLocation)
    {
        _spawnUsed.Remove(_refLocation);
    }

}
