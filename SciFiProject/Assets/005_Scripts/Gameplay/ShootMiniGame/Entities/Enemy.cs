using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Fields

    private Vector3 velocity = Vector3.zero;

    #endregion

    #region Properties

    public Rigidbody2D rb { get; set; }

    public EnemyStats enemyStats { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private Vector3 direction;

    #endregion

    #region Behaviour

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, direction * enemyStats.Speed, ref velocity, 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.type == Entity.Type.Player)
        {
            typeCollision.GetComponent<PlayerStats>().TakeShootDamage(-enemyStats.Damage);

            Destroy(gameObject);
        }
    }

    #endregion
}
