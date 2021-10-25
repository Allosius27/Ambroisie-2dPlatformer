using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopeMiniGameManager : AllosiusDev.Singleton<DopeMiniGameManager>
{
    #region Fields

    private float baseCountTime;

    private bool timerActive;

    #endregion

    #region Properties

    public float MultiplierCountTimePerLevel => multiplierCountTimePerLevel;

    public List<DopeLevelRank> LevelRanks => levelRanks;

    public PlayerStats playerStats { get; protected set; }

    #endregion

    #region UnityInspector

    [SerializeField] private float countTime;

    [SerializeField] private float multiplierCountTimePerLevel = 1.0f;

    [SerializeField] private List<DopeLevelRank> levelRanks = new List<DopeLevelRank>();

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        baseCountTime = countTime;

        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpBar.SetMaxBarValue(playerStats.currentDopeJobExpRequired);
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpBar.SetBarValue(playerStats.currentDopeJobExp);
        SetCurrentExpRank(playerStats.currentDopeJobExp, playerStats.currentDopeJobExpRequired);

        SetCurrentLevelRankTitle(playerStats.currentDopeJobLevel);
    }

    private void Update()
    {
        if (GameCore.Instance.dopesMiniGameActive)
        {
            countTime -= Time.deltaTime;
            UpdateCountText();
        }
    }

    public void ReinitCountTime()
    {
        countTime = baseCountTime;
        timerActive = true;
    }

    public void ChangeCountTime(float amount)
    {
        baseCountTime *= amount;
    }

    private void UpdateCountText()
    {
        TimeSpan time = TimeSpan.FromSeconds(countTime);

        string str = time.ToString(@"mm\:ss");

        if (countTime <= 0 && timerActive)
        {
            countTime = 0;
            timerActive = false;
            StartCoroutine(EndDopeMiniGame());
        }

        GameCore.Instance.GetGameCanvasManager().Dopes.DopeTimer.transform.GetChild(0).GetComponent<Text>().text = str;

    }

    public int SetCurrentTotalPrestigePointsGained()
    {
        int _prestige = 0;

        for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
        {
            if (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].sliderEmpty == false)
            {
                _prestige += (int)(GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].PrestigePointsGainedMultiplier *
                (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.maxValue - GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.value));
            }
        }

        return _prestige;
    }

    public int SetCurrentTotalExpPointsGained()
    {
        int _exp = 0;

        for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
        {
            if (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].sliderEmpty == false && GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].isActive)
            {
                _exp += (int)(GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].ExpPointsGainedMultiplier *
                (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.maxValue - GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.value));
            }
        }
        Debug.Log(_exp);
        return _exp;
    }

    public void SetCurrentLevelRankTitle(int playerJobLevel)
    {
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpLabelText.text = levelRanks[playerJobLevel].title + " :";
    }

    public void SetCurrentNumberDopesSlidersActives(int playerJobLevel)
    {
        GameCore.Instance.GetGameCanvasManager().Dopes.NumberdopesSlidersActives = levelRanks[playerJobLevel].numberDopesSliderActives;
    }

    public void SetCurrentExpRank(float playerCurrentJobExp, float playerCurrentJobExpRequired)
    {
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpBar.SetBarValue(playerCurrentJobExp);
        int _playerCurrentJobExp = (int)(playerCurrentJobExp);
        int _playerCurrentJobExpRequired = (int)(playerCurrentJobExpRequired);
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpAmountText.text = _playerCurrentJobExp.ToString() + "/" + _playerCurrentJobExpRequired;
    }

    public IEnumerator EndDopeMiniGame()
    {
        playerStats.ChangeDopeJobExp(SetCurrentTotalExpPointsGained());
        playerStats.ChangePrestigePoints(SetCurrentTotalPrestigePointsGained());
        GameCore.Instance.SetPrestigeAmount(playerStats.PrestigePoints);

        yield return new WaitForSeconds(1.5f);

        GameCore.Instance.SetStateDopeMiniGame(false);
    }

    #endregion
}
