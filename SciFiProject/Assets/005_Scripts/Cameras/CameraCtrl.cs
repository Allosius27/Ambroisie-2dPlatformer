using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    #region Fields

    private Vector3 velocity;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject player;

    [Space]

    [SerializeField] private float timeOffset;
    [SerializeField] private Vector3 posOffset;


    #endregion

    private void Awake()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>().gameObject;
        }
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
