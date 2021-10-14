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

    public StatBar ShootHealthBar => shootHealthBar;
    public StatBar ShootJobExpBar => shootJobExpBar;
    public Text ShootJobExpLabelText => shootJobExpLabelText;
    public Text ShootJobExpAmountText => shootJobExpAmountText;

    public Text DateAmountText => dateAmountText;

    public GameObject BabiesFactoryTimer => babiesFactoryTimer;

    #endregion

    #region UnityInspector

    [SerializeField] private StatBar healthBar;
    [SerializeField] private StatBar moodBar;

    [SerializeField] private StatBar shootHealthBar;
    [SerializeField] private StatBar shootJobExpBar;
    [SerializeField] private Text shootJobExpLabelText;
    [SerializeField] private Text shootJobExpAmountText;

    [SerializeField] private Text dateAmountText;

    [SerializeField] private Text prestigeAmountText;

    [SerializeField] private GameObject babiesFactoryTimer;

    #endregion

    #region Behaviour

    public void SetPrestigeAmountText(int amount)
    {
        prestigeAmountText.text = amount.ToString();
    }

    #endregion
}
