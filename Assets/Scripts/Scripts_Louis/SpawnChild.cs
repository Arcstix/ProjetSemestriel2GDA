using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script attaché au Player va gérer le spawn des entités derrière le Player
public class SpawnChild : MonoBehaviour
{
    // Une liste de Transform pour le spawn d'entité.
    [SerializeField] private List<Transform> _spawnList = new List<Transform>();
    [SerializeField] private GameObject _childEntity;

    // Cette liste va permettre de savoir les spawner qui sont utilisés.
    private List<Transform> _spawnUsed = new List<Transform>();

    
    /// <summary>
    /// Spawn une entité dans l'un des spawns libres du Player.
    /// </summary>
    public void SpawnEntity()
    {
        foreach (var spawn in _spawnList)
        {
            // On vérifie si le spawn est utilisé.
            if (!_spawnUsed.Contains(spawn))
            {
                _spawnUsed.Add(spawn); // On ajoute le spawn qui va être utilisé à la liste des spawn utilisé.
                Instantiate(_childEntity, spawn);
                return; // On quitte la méthode plus tôt car la méthode a réalisé ce qu'on veut.
            }
        }
    }

}
