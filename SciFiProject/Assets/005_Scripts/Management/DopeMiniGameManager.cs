using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopeMiniGameManager : AllosiusDev.Singleton<DopeMiniGameManager>
{
    #region Fields

    private float baseCountTime;

    #endregion

    #region Properties

    public float MultiplierCountTimePerLevel => multiplierCountTimePerLevel;

    public List<LevelRank> LevelRanks => levelRanks;

    public PlayerStats playerStats { get; protected set; }

    #endregion

    #region UnityInspector

    [SerializeField] private float countTime;

    [SerializeField] private float multiplierCountTimePerLevel = 1.0f;

    [SerializeField] private List<LevelRank> levelRanks = new List<LevelRank>();

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
    }

    public void ChangeCountTime(float amount)
    {
        baseCountTime *= amount;
    }

    private void UpdateCountText()
    {
        TimeSpan time = TimeSpan.FromSeconds(countTime);

        string str = time.ToString(@"mm\:ss");

        if (countTime <= 0)
        {
            countTime = 0;

            StartCoroutine(EndDopeMiniGame());
        }

        GameCore.Instance.GetGameCanvasManager().Dopes.DopeTimer.transform.GetChild(0).GetComponent<Text>().text = str;

    }

    public int SetCurrentTotalExpPointsGained()
    {
        int _exp = 0;

        for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
        {
            _exp += (int)(GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].ExpPointsGainedMultiplier *
                (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.maxValue - GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].Slider.value));
        }

        return _exp;
    }

    public void SetCurrentLevelRankTitle(int playerJobLevel)
    {
        GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpLabelText.text = levelRanks[playerJobLevel].title + " :";
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
        playerStats.ChangeShootJobExp(SetCurrentTotalExpPointsGained());

        yield return new WaitForSeconds(1.5f);

        GameCore.Instance.SetStateDopeMiniGame(false);
    }

    #endregion
}
