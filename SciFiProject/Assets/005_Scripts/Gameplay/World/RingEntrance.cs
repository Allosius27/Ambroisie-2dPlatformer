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
            GameCore.Instance.MainCameraCtrl.gameObject.SetActive(false);
            GameCore.Instance.ShootCameraCtrl.gameObject.SetActive(true);

            GameCore.Instance.WorldHub.SetActive(false);
            GameCore.Instance.WorldShootMiniGame.SetActive(true);

            GameCore.Instance.GetGameCanvasManager().HealthBar.gameObject.SetActive(false);
            GameCore.Instance.GetGameCanvasManager().ShootHealthBar.gameObject.SetActive(true);


            Player _player = FindObjectOfType<Player>();
            _player.canControl = false;
            _player.transform.position = GameCore.Instance.ShootMiniGamePlayerSpawnPoint.transform.position;
            GameCore.Instance.GetGameCanvasManager().ShootHealthBar.SetMaxBarValue(_player.GetComponent<PlayerStats>().ShootHealth);

            GameCore.Instance.shootMiniGameActive = true;
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
