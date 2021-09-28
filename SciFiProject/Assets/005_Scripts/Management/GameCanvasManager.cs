using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    public StatBar HealthBar => healthBar;
    public StatBar MoodBar => moodBar;

    public Text DateAmountText => dateAmountText;

    #endregion

    #region UnityInspector

    [SerializeField] private StatBar healthBar;
    [SerializeField] private StatBar moodBar;

    [SerializeField] private Text dateAmountText;

    #endregion
}
