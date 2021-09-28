using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Fields

    private float countTimer;
    private int currentYearsAmount;

    private GameCanvasManager gameCanvasManager;

    #endregion

    #region Properties

    public bool timerActive { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private int timeInterval;
    [SerializeField] private int yearsRank;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        gameCanvasManager = FindObjectOfType<GameCanvasManager>();

        timerActive = true;
    }

    private void Start()
    {
        gameCanvasManager.DateAmountText.text = currentYearsAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            AddCountTime(Time.deltaTime);
        }
    }


    public void AddCountTime(float amount)
    {
        countTimer += amount;

        if (countTimer >= timeInterval)
        {
            countTimer = 0.0f;
            currentYearsAmount += yearsRank;
            gameCanvasManager.DateAmountText.text = currentYearsAmount.ToString();
        }
    }
}
