using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    #region Fields


    #endregion

    #region Properties
    public float damage { get; set; }
    #endregion

    #region Behaviour

    public override void Start()
    {
        base.Start();

        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if(typeCollision.type == Entity.Type.Enemy)
        {
            typeCollision.GetComponent<EnemyStats>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    #endregion
}
