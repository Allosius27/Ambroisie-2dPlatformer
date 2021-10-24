using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderMachine : Entrance
{
    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxEnterMiniGame;

    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey) && CanLaunch)
        {
            AllosiusDev.AudioManager.Play(sfxEnterMiniGame.sound);
            GameCore.Instance.GetGameCanvasManager().ChooseGender.ActiveChooseGenderMenu(true);
        }
    }
}
