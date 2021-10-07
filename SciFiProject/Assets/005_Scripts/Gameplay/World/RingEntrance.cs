using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEntrance : MonoBehaviour
{
    #region Fields

    private bool canLaunchMiniGame;

    #endregion

    #region UnityInspector

    [SerializeField] private KeyCode activeMiniGameKey = KeyCode.E;
    [SerializeField] private GameObject displayKeyMiniGameToInput;

    #endregion

    private void Start()
    {
        displayKeyMiniGameToInput.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(activeMiniGameKey) && canLaunchMiniGame)
        {
            GameCore.Instance.SetStateShootMiniGame(true);
        }

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
