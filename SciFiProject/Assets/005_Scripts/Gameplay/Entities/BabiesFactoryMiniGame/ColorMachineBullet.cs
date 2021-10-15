using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachineBullet : Bullet
{
    #region Properties

    public Color bulletColor { get; set; }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if(typeCollision.type == Entity.Type.ColorCapsule)
        {
            typeCollision.GetComponent<SpriteRenderer>().color = bulletColor;
            BabiesFactoryMiniGameManager.Instance.MoveTapis();
            BabiesFactoryMiniGameManager.Instance.ColorMachine.SetColorSquare();
            BabiesFactoryMiniGameManager.Instance.ColorsTouchs.SetColorsTouchs();
        }

        Destroy(gameObject);
    }
}
