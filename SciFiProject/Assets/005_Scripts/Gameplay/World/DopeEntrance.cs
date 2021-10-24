using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DopeEntrance : Entrance
{
    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxEnterMiniGame;

    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey) && CanLaunch)
        {
            Debug.Log("Dope Entrance");
            AllosiusDev.AudioManager.Play(sfxEnterMiniGame.sound);
            GameCore.Instance.SetStateDopeMiniGame(true);
        }
    }
}
