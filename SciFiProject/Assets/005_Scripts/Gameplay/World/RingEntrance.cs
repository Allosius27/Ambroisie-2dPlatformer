using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEntrance : Entrance
{
    #region Fields


    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxEnterMiniGame;

    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey) && CanLaunchMiniGame)
        {
            AllosiusDev.AudioManager.Play(sfxEnterMiniGame.sound);
            GameCore.Instance.SetStateShootMiniGame(true);
        }
    }
}
