using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Instanciation : MonoBehaviour
{
    public float timer = 0;

    public GameObject _instanciation;
    public GameObject _instanciation2;
    public GameObject _Collectible;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            _instanciation.SetActive(false);
            _instanciation2.SetActive(true);
            _Collectible.SetActive(true);
        }
    }
}
