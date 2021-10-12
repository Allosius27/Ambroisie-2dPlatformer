using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magasin : MonoBehaviour
{
    #region Fields

    private Shop shop;

    private bool canOpenShopMenu;

    #endregion

    #region Properties

    public Transform ColisRobotSpawnPoint => colisRobotSpawnPoint;

    #endregion

    #region UnityInspector

    [SerializeField] private KeyCode activeShopMenuKey = KeyCode.E;
    [SerializeField] private GameObject displayKeyShopMenuToInput;

    [SerializeField] private Transform colisRobotSpawnPoint;

    #endregion

    #region Behaviour

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
    }

    private void Start()
    {
        displayKeyShopMenuToInput.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(activeShopMenuKey) && canOpenShopMenu)
        {
            shop.ActiveShopMenu(true);
            shop.magasin = this;

            Player _player = FindObjectOfType<Player>();
            _player.canControl = false;
        }

        if(canOpenShopMenu && GameCore.Instance.shopMenuIsOpen == false)
        {
            displayKeyShopMenuToInput.SetActive(true);
        }
        else
        {
            displayKeyShopMenuToInput.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.type == Entity.Type.Player)
        {
            canOpenShopMenu = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.type == Entity.Type.Player)
        {
            canOpenShopMenu = false;
        }
    }

    #endregion
}
