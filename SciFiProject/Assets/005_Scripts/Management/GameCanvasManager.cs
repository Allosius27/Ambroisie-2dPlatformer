using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    public ChooseGender ChooseGender => chooseGender;

    public StatBar HealthBar => healthBar;
    public StatBar MoodBar => moodBar;

    public StatBar ShootHealthBar => shootHealthBar;
    public StatBar ShootJobExpBar => shootJobExpBar;
    public Text ShootJobExpLabelText => shootJobExpLabelText;
    public Text ShootJobExpAmountText => shootJobExpAmountText;

    public Text DateAmountText => dateAmountText;

    public GameObject BabiesFactoryTimer => babiesFactoryTimer;
    public StatBar BabiesJobExpBar => babiesJobExpBar;
    public Text BabiesJobExpLabelText => babiesJobExpLabelText;
    public Text BabiesJobExpAmountText => babiesJobExpAmountText;

    public Dopes Dopes => dopes;

    #endregion

    #region UnityInspector

    [SerializeField] private ChooseGender chooseGender;

    [Space]

    [SerializeField] private StatBar healthBar;
    [SerializeField] private StatBar moodBar;

    [Space]

    [SerializeField] private StatBar shootHealthBar;
    [SerializeField] private StatBar shootJobExpBar;
    [SerializeField] private Text shootJobExpLabelText;
    [SerializeField] private Text shootJobExpAmountText;

    [Space]

    [SerializeField] private Text dateAmountText;

    [SerializeField] private Text prestigeAmountText;

    [Space]

    [SerializeField] private GameObject babiesFactoryTimer;
    [SerializeField] private StatBar babiesJobExpBar;
    [SerializeField] private Text babiesJobExpLabelText;
    [SerializeField] private Text babiesJobExpAmountText;

    [Space]

    [SerializeField] private Dopes dopes;

    [Space]

    [SerializeField] private Animator fadingImageAnimator;

    #endregion

    #region Behaviour

    public void SetPrestigeAmountText(int amount)
    {
        prestigeAmountText.text = amount.ToString();
    }

    public void LaunchFadeImage()
    {
        fadingImageAnimator.SetTrigger("Fade");
    }

    #endregion
}
