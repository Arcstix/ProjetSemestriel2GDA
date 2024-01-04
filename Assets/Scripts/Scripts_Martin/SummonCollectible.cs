using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Ce script va permettre d'instantier un collectible par le prefab indicateur d'une apparition.
public class SummonCollectible : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToInstantiate;
    [SerializeField] private VisualEffect _summonEffect;

    private float _lifeTime;
    private float timer;
    private SpawnerEntity _spawnerRef;

    private void Awake()
    {
        _lifeTime = _summonEffect.GetFloat("LifeTime");
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(_lifeTime < timer)
        {
            GameObject newInstance = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
            if(newInstance.TryGetComponent<CollectibleVFX>(out CollectibleVFX collectible))
            {
                collectible.SetSpawnerRef(_spawnerRef);
            }
            Destroy(gameObject);
        }
    }

    // Permet de garder une référence au script qui l'a instancié et le passera ensuite au collectible.
    public void SetSpawnerRef(SpawnerEntity spawnerEntity)
    {
        _spawnerRef = spawnerEntity;
    }
}
