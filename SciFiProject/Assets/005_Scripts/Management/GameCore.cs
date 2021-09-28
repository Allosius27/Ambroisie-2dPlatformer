using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Fields

    private GameCanvasManager gameCanvasManager;
    private PlayerStats playerStats;

    private float countTimer;

    #endregion

    #region Properties

    #endregion

    #region UnityInspector

    [SerializeField] private int moodTimeInterval;
    [SerializeField] private int moodLostPerTimeInterval;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        gameCanvasManager = FindObjectOfType<GameCanvasManager>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        gameCanvasManager.MoodBar.SetMaxBarValue(playerStats.Mood);
        gameCanvasManager.HealthBar.SetMaxBarValue(playerStats.Health);
    }

    private void Update()
    {
        AddCountTime(Time.deltaTime);
    }

    public void AddCountTime(float amount)
    {
        countTimer += amount;

        if (countTimer >= moodTimeInterval)
        {
            countTimer = 0.0f;
            int newPlayerMoodValue = playerStats.ChangeMood(moodLostPerTimeInterval);
            gameCanvasManager.MoodBar.SetBarValue(newPlayerMoodValue);
        }
    }

    #endregion
}
