using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb;

    #endregion

    #region Properties

    private Vector3 velocity = Vector3.zero;
    public Vector3 direction { get; set; }

    public float speed { get; set; }
    #endregion

    #region Behaviour

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();;
    }

    public virtual void Update()
    {
        //transform.position += direction * Time.deltaTime * speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, direction * speed, ref velocity, 0.05f);
    }

    #endregion
}
