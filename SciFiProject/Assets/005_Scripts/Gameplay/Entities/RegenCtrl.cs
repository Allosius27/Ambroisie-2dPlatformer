using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenCtrl : MonoBehaviour
{
    public float baseSpeed { get; protected set; }

    private Vector3 basePosition;

    public bool hasHealPlayer { get; protected set; }

    public ArmCtrl armCtrl;
    public Transform pivotPoint;
    public Transform target { get; set; }

    public Animator anim;

    public float timeToRegen;

    public float speed;

    public bool isCapsuleHealth;

    private void Awake()
    {
        baseSpeed = speed;

        basePosition = transform.position;
    }

    private void Start()
    {
        if(isCapsuleHealth)
        {
            anim.enabled = false;
        }

        if(!isCapsuleHealth)
        {
            Player _player = FindObjectOfType<Player>();
            _player.canControl = false;
        }
    }

    private void Update()
    {
        target = GameCore.Instance.PlayerStats.transform;

        if (!hasHealPlayer)
        {
            Player _player = FindObjectOfType<Player>();
            _player.canControl = false;

            Vector3 dir = target.position - transform.position;
            dir.Normalize();
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime * dir.y, transform.position.z);

            if (pivotPoint.position.y - target.position.y < 0.3f && speed != 0)
            {
                speed = 0;

                if (isCapsuleHealth)
                {
                    anim.enabled = true;
                }

                armCtrl.regenCtrl = this;
                armCtrl.canMove = true;
            }
        }
        else
        {
            Vector3 dir = basePosition - transform.position;
            dir.Normalize();
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime * dir.y, transform.position.z);

            if(speed != 0)
            {
                armCtrl.speed = 0;
            }

            if (Vector3.Distance(transform.position, basePosition) < 0.3f)
            {
                speed = 0;

                Destroy(gameObject);
            }
        }
    }

    public IEnumerator RegenTimer()
    {
        yield return new WaitForSeconds(timeToRegen);

        hasHealPlayer = true;
        
        armCtrl.speed = armCtrl.baseSpeed;

        yield return new WaitForSeconds(1f);

        if (isCapsuleHealth)
            GameCore.Instance.PlayerStats.PlayerResurect();
        else
        {
            GameCore.Instance.PlayerStats.GetComponent<Player>().canControl = true;
            GameCore.Instance.capsuleMoodInstance = false;
        }
    }
}
