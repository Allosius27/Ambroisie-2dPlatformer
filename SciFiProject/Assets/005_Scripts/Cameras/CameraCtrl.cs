using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    #region Fields

    private Vector3 velocity;

    #endregion

    #region Properties

    public bool triggerX { get; set; }

    public bool triggerY { get; set; }

    public GameObject Player => player;

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
        if (!triggerX && !triggerY)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        }
        else if(triggerX)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, player.transform.position.y + posOffset.y, player.transform.position.z + posOffset.z), ref velocity, timeOffset);
        }
        else if(triggerY)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x + posOffset.x, transform.position.y, player.transform.position.z + posOffset.z), ref velocity, timeOffset);
        }
    }

    public void SetPlayer(GameObject _object)
    {
        player = _object;
    }
}
