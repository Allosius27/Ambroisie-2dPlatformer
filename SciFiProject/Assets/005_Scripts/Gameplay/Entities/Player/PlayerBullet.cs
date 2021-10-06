using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    #region Fields

    private Rigidbody2D rb;

    #endregion

    #region Properties

    private Vector3 velocity = Vector3.zero;
    public Vector3 direction { get; set; }

    public float damage { get; set; }
    public float speed { get; set; }
    #endregion

    #region Behaviour

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        //transform.position += direction * Time.deltaTime * speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, direction * speed, ref velocity, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        Destroy(gameObject);
    }

    #endregion
}
