using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dopes : MonoBehaviour
{
    #region Properties

    public List<DopeSlider> DopesSliders => dopesSliders;

    public GameObject DopeTimer => dopeTimer;

    public StatBar DopeJobExpBar => dopeJobExpBar;
    public Text DopeJobExpLabelText => dopeJobExpLabelText;
    public Text DopeJobExpAmountText => dopeJobExpAmountText;

    #endregion

    #region UnityInspector

    [SerializeField] private List<DopeSlider> dopesSliders = new List<DopeSlider>();

    [SerializeField] private GameObject dopeTimer;

    [SerializeField] private StatBar dopeJobExpBar;
    [SerializeField] private Text dopeJobExpLabelText;
    [SerializeField] private Text dopeJobExpAmountText;

    #endregion
}
