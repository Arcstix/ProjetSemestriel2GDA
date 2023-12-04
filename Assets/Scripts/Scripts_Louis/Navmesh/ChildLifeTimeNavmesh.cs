using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLifeTimeNavmesh : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 10f;

    private SpawnChildNavmesh _refSpawner; // Va permettre de prévenir le spawner quand la durée de vie prend fin.
    private Transform _refLocation; // Va permettre d'avoir une ref par rapport à l'emplacement utilisé.

    /// <summary>
    /// Démarre la durée de vie de l'entité. Il possède en paramètre le script qui l'a instantité et la position où il est instantité.
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
    /// Permet de retirer de la liste l'entité car disparais.
    /// </summary>
    private void EndOfLifeTime()
    {
        _refSpawner.ClearLocation(_refLocation);
        Destroy(this.gameObject);
    }
}
