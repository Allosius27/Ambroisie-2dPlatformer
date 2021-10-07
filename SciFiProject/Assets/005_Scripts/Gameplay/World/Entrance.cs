using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    #region Fields

    private bool canLaunchMiniGame;

    #endregion

    #region Properties

    public bool CanLaunchMiniGame => canLaunchMiniGame;

    public KeyCode ActiveMiniGameKey => activeMiniGameKey;
    public GameObject DisplayKeyMiniGameToInput => displayKeyMiniGameToInput;

    #endregion

    #region UnityInspector

    [SerializeField] private KeyCode activeMiniGameKey = KeyCode.E;
    [SerializeField] private GameObject displayKeyMiniGameToInput;

    #endregion

    public virtual void Start()
    {
        displayKeyMiniGameToInput.SetActive(false);
    }

    public virtual void Update()
    {
        if (canLaunchMiniGame)
        {
            displayKeyMiniGameToInput.SetActive(true);
        }
        else
        {
            displayKeyMiniGameToInput.SetActive(false);
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
            canLaunchMiniGame = true;
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
            canLaunchMiniGame = false;
        }
    }
}
