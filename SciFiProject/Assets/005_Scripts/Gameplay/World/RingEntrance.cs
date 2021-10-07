using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEntrance : Entrance
{
    #region Fields


    #endregion

    #region UnityInspector


    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey) && CanLaunchMiniGame)
        {
            GameCore.Instance.SetStateShootMiniGame(true);
        }
    }
}
