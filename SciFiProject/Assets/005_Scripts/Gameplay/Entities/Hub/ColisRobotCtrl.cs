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
    [SerializeField] private Transform boxSpawnPoint;
    [SerializeField] private Transform pivotPoint;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject prefabColis;

    #endregion

    private void Awake()
    {
        baseSpeed = speed;

        RotateChange = new Vector3(0f, 180f, 0f);

        anim.SetBool("IsWalking", true);
    }

    private void Start()
    {
        transform.Rotate(RotateChange);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * dir.x, transform.position.y, transform.position.z);
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(pivotPoint.position, target.position) < 0.3f)
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
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        GameObject colis = Instantiate(prefabColis, boxSpawnPoint.position, boxSpawnPoint.rotation);
        Destroy(colis, 4);
        Physics2D.IgnoreCollision(colis.GetComponent<Collider2D>(), playerStats.GetComponent<Collider2D>());
        anim.SetBool("Leave", true);
        speed = baseSpeed;
        playerStats.TakeDamage(-1000);
        Destroy(gameObject, 5f);
    }
}
