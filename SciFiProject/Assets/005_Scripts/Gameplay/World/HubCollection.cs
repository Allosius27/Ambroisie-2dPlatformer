using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCollection : MonoBehaviour
{
    #region Properties

    public List<CollectionObject> CollectionsItems => collectionItems;

    #endregion

    #region UnityInspector

    [SerializeField] private List<CollectionObject> collectionItems = new List<CollectionObject>();

    #endregion

    #region Behaviour

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (collectionItems.Contains(transform.GetChild(i).GetComponent<CollectionObject>()) == false)
            {
                collectionItems.Add(transform.GetChild(i).GetComponent<CollectionObject>());
            }
        }
    }

    private void Start()
    {
        CheckBuyStateCollectionsObjects();
    }

    public void CheckBuyStateCollectionsObjects()
    {
        for (int i = 0; i < collectionItems.Count; i++)
        {
            if (collectionItems[i].isBought == false)
            {
                collectionItems[i].gameObject.SetActive(false);
            }
            else
            {
                collectionItems[i].gameObject.SetActive(true);
            }
        }
    }

    #endregion
}
