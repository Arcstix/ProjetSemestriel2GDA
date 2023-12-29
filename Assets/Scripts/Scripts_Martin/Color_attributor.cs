using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_attributor : MonoBehaviour
{
    public Color _color;
    // Start is called before the first frame update
    void Start()
    {
        _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        var renderer = GetComponent<MeshRenderer>();
        renderer.material.color = _color;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Entity") || other.CompareTag("Collectible"))
        {
            var otherRend = other.GetComponentInChildren<MeshRenderer>();
            otherRend.material.color = _color;
        }
    }
}
