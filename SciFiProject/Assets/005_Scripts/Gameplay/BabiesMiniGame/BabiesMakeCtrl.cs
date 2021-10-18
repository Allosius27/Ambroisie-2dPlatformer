using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabiesMakeCtrl : MonoBehaviour
{

    #region Fields

    private Vector3 basePosition;

    #endregion

    #region Properties

    public bool babiesSort { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    #endregion

    private void Awake()
    {
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (babiesSort)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            // Si l'ennemi est quasiment arrivé à sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                babiesSort = false;
                GameCore.Instance.SetStateBabiesMiniGame(false);

            }
        }
    }

    public void ReinitPosition()
    {
        transform.position = basePosition;
    }
}
