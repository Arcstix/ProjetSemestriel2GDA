using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va permettre de rassembler tous les collectibles en un seul Rb et les �jecter si besoin.
public class CollectibleContainer : MonoBehaviour
{
    public List<GameObject> _collectibles; // Liste des collectibles en enfant du CollectibleContainer
    public float minSwallowRelativeVelocity = 0.2f; // En dessous de cette relative v�locity les collectibles fusionnent.

    public bool scheduledDestruction = false; // Permet � ce qu'un seul des 2 CollectibleContainer se d�truisent et swallow.

    private Rigidbody rb;

    public CollectibleContainer emptyCollectibleContainerPrefab; // Prefab qui est instantit� quand un collectible est �ject�.


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_collectibles.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// M�thode appel� quand des collectibles se collisionne.
    /// </summary>
    /// <param name="otherCollectibleContainer"></param>
    public void Swallow(CollectibleContainer otherCollectibleContainer)
    {
        if (scheduledDestruction) return;

        _collectibles.AddRange(otherCollectibleContainer._collectibles); // On ajoute la liste de collectible � la liste du Container.
        foreach (GameObject collectible in otherCollectibleContainer._collectibles)
        {
            if(collectible != null)
            {
                collectible.transform.parent = transform; // Cette ligne permet de d�placer le collectible vers l'autre CollectibleContainer.
            }            
        }

        otherCollectibleContainer.scheduledDestruction = true;
        Destroy(otherCollectibleContainer.gameObject);
    }

    /// <summary>
    /// M�thode appel� quand un collectible change de cible.
    /// </summary>
    /// <param name="collectibleToEject"></param>
    public void Eject(Transform closestTarget, GameObject collectibleToEject)
    {
        if (_collectibles.Contains(collectibleToEject))
        {
            _collectibles.Remove(collectibleToEject);
        }

        CollectibleContainer newContainer = Instantiate(emptyCollectibleContainerPrefab, collectibleToEject.transform.position, collectibleToEject.transform.rotation);
        newContainer.AddCollectible(collectibleToEject);
        newContainer.GetComponent<CollectibleManager>().ReciveClosestTarget(closestTarget, collectibleToEject);

        if(_collectibles.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// M�thode qui permet de placer un collectible dans un Container.
    /// </summary>
    /// <param name="collectible"></param>
    public void AddCollectible(GameObject collectible)
    {
        _collectibles.Add(collectible);
        collectible.transform.parent = transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.isTrigger) { return; }

        // On a besoin d'�tre plus rigoureux sur les conditions avant de swallow car dans l'�tat �a ne fonctionne pas.
        // On CollisionStay n'est pas suffisant car il est appel� tout le temps par tous les collectibles.
        // Il faudrait que une fois que les conditions sont remplies apr�s un instant T on n'ai plus besoin de faire de v�rification.
        // Mais que si les conditions ne sont plus remplies les v�rifications se fassent. (c'est des hypoth�ses)
        if(other.collider.CompareTag("Blue") || other.collider.CompareTag("Red"))
        {
            Swallow(other.rigidbody.GetComponent<CollectibleContainer>());
        }
    }
}
