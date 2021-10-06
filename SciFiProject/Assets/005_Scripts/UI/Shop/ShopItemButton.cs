using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    #region Fields

    private bool itemBought;

    #endregion

    #region Properties

    public CollectionObject collectionObject { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI priceText;

    #endregion

    #region Behaviour

    public void SetItemImage(Sprite _sprite)
    {
        itemImage.sprite = _sprite;
    }

    public void SetPriceText(int _amount)
    {
        priceText.text = _amount.ToString();
    }

    public void ClickShopItemButton()
    {
        if (itemBought == false)
        {
            itemBought = true;

            collectionObject.isBought = true;
            collectionObject.gameObject.SetActive(true);
        }
    }

    #endregion
}
