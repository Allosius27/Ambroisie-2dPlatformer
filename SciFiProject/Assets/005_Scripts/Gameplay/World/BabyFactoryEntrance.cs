using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyFactoryEntrance : Entrance
{
    #region Fields


    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxEnterMiniGame;

    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey) && CanLaunch)
        {
            Debug.Log("Baby Factory Entrance");
            AllosiusDev.AudioManager.Play(sfxEnterMiniGame.sound);
            GameCore.Instance.SetStateBabiesMiniGame(true);
        }
    }
}
