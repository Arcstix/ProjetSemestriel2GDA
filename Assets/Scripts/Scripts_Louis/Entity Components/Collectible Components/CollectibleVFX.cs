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

    // M�thode appel� quand le collectible est d�truit
    public void PlayVFXDestroy()
    {
        Instantiate(_destroyCollectible, transform.position, Quaternion.identity);
        // On d�cr�mente le nombre d'instance qui se trouve dans la sc�ne.
        if(_spawnerRef != null)
        {
            _spawnerRef.CurrentNumberOfInstantiation--;
        }       
        Destroy(gameObject);
    }

    // M�thode appel� quand le collectible d�tecte une entit�. Un event est appel� dans le VisualEffect pour jouer un VFX.
    public void PlayVFXDetection()
    {
        _visualEffect.SendEvent("OnDetection");
    }

    // M�thode qui sert � avoir une r�f�rence au spawner pour qu'� la destruction on puisse avertir le spawner qu'il y a une instance en moins dans la sc�ne.
    public void SetSpawnerRef(SpawnerEntity spawnerRef)
    {
        _spawnerRef = spawnerRef;
    }
}
