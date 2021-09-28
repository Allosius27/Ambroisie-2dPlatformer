using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    #region Fields

    private CameraCtrl cameraCtrl;

    private Player player;

    #endregion

    #region UnityInspector
    public enum TypeTrigger
    {
        triggerX,
        triggerY,
    }
    public TypeTrigger Type;

    #endregion

    private void Awake()
    {
        cameraCtrl = FindObjectOfType<CameraCtrl>();

        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Camera Collision");

        if(collision.gameObject == player.gameObject)
        {
            Debug.Log("Trigger Camera Collides Player");

            if(Type == TypeTrigger.triggerX)
            {
                cameraCtrl.triggerX = true;
            }
            else if (Type == TypeTrigger.triggerY)
            {
                cameraCtrl.triggerY = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Camera Exit Collision");

        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("Trigger Camera Exit Collides Player");

            if (Type == TypeTrigger.triggerX)
            {
                cameraCtrl.triggerX = false;
            }
            else if (Type == TypeTrigger.triggerY)
            {
                cameraCtrl.triggerY = false;
            }
        }
    }
}
