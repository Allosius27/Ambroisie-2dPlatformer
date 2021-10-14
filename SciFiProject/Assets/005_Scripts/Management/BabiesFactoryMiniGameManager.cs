using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabiesFactoryMiniGameManager : AllosiusDev.Singleton<BabiesFactoryMiniGameManager>
{
    #region Fields

    private float baseCountTime;

    #endregion

    #region Properties

    public GameObject ColorTapis => colorTapis;
    public GameObject ColorMachine => colorMachine;

    public ColorsTouchs ColorsTouchs => colorsTouchs;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject colorTapis;
    [SerializeField] private GameObject colorMachine;

    [SerializeField] private ColorsTouchs colorsTouchs;

    [SerializeField] private float countTime;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        baseCountTime = countTime;
    }

    public void ReinitCountTime()
    {
        countTime = baseCountTime;
    }

    private void Update()
    {
        if(GameCore.Instance.babiesFactoryMiniGameActive)
        {
            countTime -= Time.deltaTime;
            UpdateCountText();
        }
    }

    private void UpdateCountText()
    {
        TimeSpan time = TimeSpan.FromSeconds(countTime);

        string str = time.ToString(@"mm\:ss");

        if(countTime <= 0)
        {
            countTime = 0;
        }

        GameCore.Instance.GetGameCanvasManager().BabiesFactoryTimer.transform.GetChild(0).GetComponent<Text>().text = str;

    }

    #endregion
}
