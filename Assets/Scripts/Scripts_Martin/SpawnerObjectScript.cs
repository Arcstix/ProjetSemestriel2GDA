using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

//ce Script permet de spawner plusieurs types d'objets dans un espace aléatoirement
public class SpawnerObjectScript : MonoBehaviour
{
    //
    public float _Timer = 0f;
    public float _spawnrate = 2f;

    Vector3 m_min, m_max;
    Collider m_collider;

    public GameObject[] _Entity;
    
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();

        m_min = m_collider.bounds.min;
        m_max = m_collider.bounds.max;
    }

    // Un Timer qui spawn un objet  dès qu'il atteint la limite
    void FixedUpdate()
    {
        if (_Timer < _spawnrate)
        {
            _Timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            _Timer = 0;
        }
    }

    //Cette méthode choisi aléatoirement un objet de l'index puis le spawn à une position aléatoire à l'intérieur du collider
    public void Spawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, _Entity.Length);

        Vector3 SpawnPos = new Vector3(UnityEngine.Random.Range(m_min.x, m_max.x),0, UnityEngine.Random.Range(m_min.z, m_max.z));
        Instantiate(_Entity[randomIndex], SpawnPos, Quaternion.identity);
    }  
}
