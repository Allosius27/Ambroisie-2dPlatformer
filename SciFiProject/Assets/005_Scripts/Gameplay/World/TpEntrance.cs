using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpEntrance : Entrance
{
    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxEnterMiniGame;

    [SerializeField] private Transform tpPoint;

    #endregion

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(ActiveMiniGameKey))
        {
            AllosiusDev.AudioManager.Play(sfxEnterMiniGame.sound);
            transform.position = tpPoint.position;
        }
    }
}
