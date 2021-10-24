using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCtrl : MonoBehaviour
{
    public float baseSpeed { get; protected set; }

    private Vector3 basePosition;

    public RegenCtrl regenCtrl { get; set; }
    public bool canMove { get; set; }
    public Transform target;

    public float speed;

    private void Awake()
    {
        baseSpeed = speed;

        basePosition = transform.localPosition;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * dir.x, transform.position.y, transform.position.z);

            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                speed = 0;

                canMove = false;
                StartCoroutine(regenCtrl.RegenTimer());
            }
        }

        if(regenCtrl != null && regenCtrl.hasHealPlayer)
        {
            Vector3 dir = basePosition - transform.localPosition;
            dir.Normalize();
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * dir.x, transform.position.y, transform.position.z);

            if (Vector3.Distance(transform.localPosition, basePosition) < 0.3f)
            {
                speed = 0;
                Debug.Log("stop");
                regenCtrl.speed = regenCtrl.baseSpeed;
            }
        }
    }
}
