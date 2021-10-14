using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyFactoryEntrance : Entrance
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
            Debug.Log("Baby Factory Entrance");
            GameCore.Instance.SetStateBabiesMiniGame(true);
        }
    }
}
