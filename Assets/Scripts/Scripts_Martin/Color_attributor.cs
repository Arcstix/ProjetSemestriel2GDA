using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_attributor : MonoBehaviour
{
    public ColorChange _Color_S;
    public Color _color;
    // Start is called before the first frame update
    void Start()
    {
        _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        var _Renderer = GetComponent<MeshRenderer>();
        _Renderer.material.color = _color;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Entity") || other.CompareTag("Collectible"))
        {
            var _Rend = other.GetComponentInChildren<MeshRenderer>();
            _Rend.material.color = _color;
        }
    }
}
