using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exemple : MonoBehaviour
{
    public Transform _target;

    private void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}