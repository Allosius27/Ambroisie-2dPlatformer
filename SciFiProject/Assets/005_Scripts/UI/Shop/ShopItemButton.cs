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

    public Shop shop { get; set; }

    public CollectionObject collectionObject { get; set; }

    public int cost { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private Image itemImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI buyText;

    [SerializeField] private TextMeshProUGUI purchasedText;

    [SerializeField] private Color purchasedItemColor;

    #endregion

    #region Behaviour

    private void Start()
    {
        purchasedText.enabled = false;
    }

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
        if (itemBought == false && shop.PlayerStats.PrestigePoints >= cost)
        {
            itemBought = true;

            GameCore.Instance.SetPrestigeAmount(shop.PlayerStats.ChangePrestigePoints(-cost));

            collectionObject.isBought = true;
            collectionObject.gameObject.SetActive(true);

            itemImage.color = purchasedItemColor;
            backgroundImage.color = purchasedItemColor;
            buttonImage.color = purchasedItemColor;
            priceText.color = purchasedItemColor;
            buyText.color = purchasedItemColor;

            purchasedText.enabled = true;
        }
    }

    #endregion
}
