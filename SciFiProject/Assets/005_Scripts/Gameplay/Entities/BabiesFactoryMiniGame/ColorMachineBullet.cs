using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachineBullet : Bullet
{
    #region Properties

    public Color bulletColor { get; set; }

    public int PrestigePointsAtGained { get; set; }
    public int ExpJobAtGained { get; set; }

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

            CapsuleFilledRewards();
        }

        Destroy(gameObject);
    }

    public void CapsuleFilledRewards()
    {
        BabiesFactoryMiniGameManager.Instance.prestigePointsGained += PrestigePointsAtGained;
        BabiesFactoryMiniGameManager.Instance.expPointsGained += ExpJobAtGained;

        BabiesFactoryMiniGameManager.Instance.playerStats.ChangeBabiesFactoryJobExp(BabiesFactoryMiniGameManager.Instance.expPointsGained);
        BabiesFactoryMiniGameManager.Instance.expPointsGained = 0;

        GameCore.Instance.SetPrestigeAmount(BabiesFactoryMiniGameManager.Instance.playerStats.ChangePrestigePoints(BabiesFactoryMiniGameManager.Instance.prestigePointsGained));
        BabiesFactoryMiniGameManager.Instance.prestigePointsGained = 0;

    }
}
