using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLifeTimeNavmesh : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 10f;

    private SpawnChildNavmesh _refSpawner; // Va permettre de pr�venir le spawner quand la dur�e de vie prend fin.
    private Transform _refLocation; // Va permettre d'avoir une ref par rapport � l'emplacement utilis�.

    /// <summary>
    /// D�marre la dur�e de vie de l'entit�. Il poss�de en param�tre le script qui l'a instantit� et la position o� il est instantit�.
    /// </summary>
    /// <param name="spawner"></param>
    /// <param name="refLocation"></param>
    public void BeginLifeTime(SpawnChildNavmesh spawner, Transform refLocation)
    {
        _refSpawner = spawner;
        _refLocation = refLocation;
        Invoke("EndOfLifeTime", _lifeTime);
    }

    /// <summary>
    /// Permet de retirer de la liste l'entit� car disparais.
    /// </summary>
    private void EndOfLifeTime()
    {
        _refSpawner.ClearLocation(_refLocation);
        Destroy(this.gameObject);
    }
}
