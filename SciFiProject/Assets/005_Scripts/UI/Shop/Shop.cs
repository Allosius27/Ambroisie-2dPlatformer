using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Fields

    private HubCollection hubCollection;

    private PlayerStats playerStats;

    #endregion

    #region Properties

    public PlayerStats PlayerStats => playerStats;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject scrollViewShopObject;
    [SerializeField] private GameObject scrollViewContent;

    [SerializeField] private ShopItemButton prefabItemToBuyButton;

    #endregion

    #region Behaviour

    private void Awake()
    {
        hubCollection = FindObjectOfType<HubCollection>();

        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        for (int i = 0; i < hubCollection.CollectionsItems.Count; i++)
        {
            ShopItemButton _shopItemButton = Instantiate(prefabItemToBuyButton);
            _shopItemButton.transform.SetParent(scrollViewContent.transform);
            _shopItemButton.transform.localPosition = Vector3.zero;
            _shopItemButton.transform.localRotation = Quaternion.identity;

            _shopItemButton.collectionObject = hubCollection.CollectionsItems[i];
            _shopItemButton.SetItemImage(hubCollection.CollectionsItems[i].ShopItemData.sprite);
            _shopItemButton.SetPriceText(hubCollection.CollectionsItems[i].ShopItemData.costItem);
            _shopItemButton.cost = hubCollection.CollectionsItems[i].ShopItemData.costItem;

            _shopItemButton.shop = this;
        }

        ActiveShopMenu(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape") && GameCore.Instance.shopMenuIsOpen)
        {
            CloseShopMenu();
        }
    }

    public void ActiveShopMenu(bool _value)
    {
        scrollViewShopObject.SetActive(_value);
        GameCore.Instance.shopMenuIsOpen = _value;
    }

    public void CloseShopMenu()
    {
        ActiveShopMenu(false);

        Player _player = FindObjectOfType<Player>();
        _player.canControl = true;
    }

    #endregion
}
