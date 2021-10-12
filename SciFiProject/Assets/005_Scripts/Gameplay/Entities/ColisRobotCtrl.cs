using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisRobotCtrl : MonoBehaviour
{
    #region Fields

    private float baseSpeed;

    private Vector3 RotateChange;

    bool playerReached;

    #endregion

    #region Properties

    public Transform target { get; set; }

    #endregion

    #region UnityInspector

    public float speed;

    [SerializeField] private Transform destinationPoint;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject prefabColis;

    #endregion

    private void Awake()
    {
        baseSpeed = speed;

        RotateChange = new Vector3(0f, 180f, 0f);

        anim.SetBool("IsWalking", true);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            if (!playerReached)
            {
                anim.SetBool("IsWalking", false);
                speed = 0;
                target = destinationPoint;
                anim.SetTrigger("Action");
                playerReached = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void ActionColis()
    {
        Instantiate(prefabColis, transform.position, transform.rotation);
        anim.SetBool("Leave", true);
        speed = baseSpeed;
    }
}
