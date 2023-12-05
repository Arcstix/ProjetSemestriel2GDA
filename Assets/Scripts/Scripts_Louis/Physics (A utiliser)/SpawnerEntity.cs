using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class SpawnerEntity : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabToInstantiate;
    [SerializeField] private float _minTime = 20f;
    [SerializeField] private float _maxTime = 40f;
    [SerializeField] private int _numberOfCollectibleAtStart = 10;
    [SerializeField] private LayerMask _groundLayer;

    private BoxCollider _collider;
    private Vector3 _minBound;
    private Vector3 _maxBound;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _minBound = _collider.bounds.min;
        _maxBound = _collider.bounds.max;

        for (int i = 0; i < _numberOfCollectibleAtStart; i++)
        {
            InstantiateEntity();
        }
        InvokeRepeating("InstantiateEntity", 0, RandomTime());
    }

    private float RandomTime()
    {
        return Random.Range(_minTime, _maxTime);
    }

    private void InstantiateEntity()
    {
        Vector3 randomPos = new Vector3(Random.Range(_minBound.x, _maxBound.x), _maxBound.y, Random.Range(_minBound.z, _maxBound.z));
        if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, _groundLayer))
        {
            int randomIndex = (int)Mathf.Round(Random.Range(0, _prefabToInstantiate.Length));
            Instantiate(_prefabToInstantiate[randomIndex], hitInfo.point, Quaternion.identity);
        }
    }
}
