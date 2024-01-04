using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CollectibleVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect _destroyCollectible;
    [SerializeField] private VisualEffect _visualEffect;

    private SpawnerEntity _spawnerRef;

    // Méthode appelé quand le collectible est détruit
    public void PlayVFXDestroy()
    {
        Instantiate(_destroyCollectible, transform.position, Quaternion.identity);
        // On décrémente le nombre d'instance qui se trouve dans la scène.
        if(_spawnerRef != null)
        {
            _spawnerRef.CurrentNumberOfInstantiation--;
        }       
        Destroy(gameObject);
    }

    // Méthode appelé quand le collectible détecte une entité. Un event est appelé dans le VisualEffect pour jouer un VFX.
    public void PlayVFXDetection()
    {
        _visualEffect.SendEvent("OnDetection");
    }

    // Méthode qui sert à avoir une référence au spawner pour qu'à la destruction on puisse avertir le spawner qu'il y a une instance en moins dans la scène.
    public void SetSpawnerRef(SpawnerEntity spawnerRef)
    {
        _spawnerRef = spawnerRef;
    }
}
