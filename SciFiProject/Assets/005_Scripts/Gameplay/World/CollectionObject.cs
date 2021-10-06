using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObject : MonoBehaviour
{
    #region Properties

    public bool isBought { get; set; }

    public ShopItemData ShopItemData => shopItemData;

    #endregion

    #region UnityInspector

    [SerializeField] private ShopItemData shopItemData;

    #endregion
}
