using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script attach� au Player va g�rer le spawn des entit�s derri�re le Player
public class SpawnChild : MonoBehaviour
{
    // Une liste de Transform pour le spawn d'entit�.
    [SerializeField] private List<Transform> _spawnList = new List<Transform>();
    [SerializeField] private ChildLifeTime _childPrefab;

    // Cette liste va permettre de savoir les spawner qui sont utilis�s.
    private List<Transform> _spawnUsed = new List<Transform>();


    /// <summary>
    /// Spawn une entit� dans l'un des spawns libres du Player.
    /// </summary>
    public void SpawnChildEntity()
    {
        foreach (var spawn in _spawnList)
        {
            // On v�rifie si le spawn est utilis�.
            if (!_spawnUsed.Contains(spawn))
            {
                _spawnUsed.Add(spawn); // On ajoute le spawn qui va �tre utilis� � la liste des spawn utilis�.
                ChildLifeTime newChild = Instantiate(_childPrefab, spawn);
                newChild.BeginLifeTime(this, spawn); // Une fois la cr�ation de l'entit� on commence la dur�e de vie de l'entit�.
                return; // On quitte la m�thode plus t�t car la m�thode a r�alis� ce qu'on veut.
            }
        }
    }

    /// <summary>
    /// Permet de retirer de la liste des spawns utilis�s les entit�s qui disparaissent.
    /// </summary>
    /// <param name="_refLocation"></param>
    public void ClearLocation(Transform _refLocation)
    {
        _spawnUsed.Remove(_refLocation);
    }

}
