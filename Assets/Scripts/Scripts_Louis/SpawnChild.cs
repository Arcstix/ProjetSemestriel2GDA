using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script attach� au Player va g�rer le spawn des entit�s derri�re le Player
public class SpawnChild : MonoBehaviour
{
    // Une liste de Transform pour le spawn d'entit�.
    [SerializeField] private List<Transform> _spawnList = new List<Transform>();
    [SerializeField] private GameObject _childEntity;

    // Cette liste va permettre de savoir les spawner qui sont utilis�s.
    private List<Transform> _spawnUsed = new List<Transform>();

    
    /// <summary>
    /// Spawn une entit� dans l'un des spawns libres du Player.
    /// </summary>
    public void SpawnEntity()
    {
        foreach (var spawn in _spawnList)
        {
            // On v�rifie si le spawn est utilis�.
            if (!_spawnUsed.Contains(spawn))
            {
                _spawnUsed.Add(spawn); // On ajoute le spawn qui va �tre utilis� � la liste des spawn utilis�.
                Instantiate(_childEntity, spawn);
                return; // On quitte la m�thode plus t�t car la m�thode a r�alis� ce qu'on veut.
            }
        }
    }

}
