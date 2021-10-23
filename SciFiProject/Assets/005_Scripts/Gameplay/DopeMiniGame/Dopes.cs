using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dopes : MonoBehaviour
{
    #region Properties

    public int NumberdopesSlidersActives { get; set; }

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

    public void SetDopesSlidersActives()
    {
        for (int i = 0; i < dopesSliders.Count; i++)
        {
            if(i < NumberdopesSlidersActives)
            {
                dopesSliders[i].isActive = true;
                dopesSliders[i].gameObject.SetActive(true);
            }
            else
            {
                dopesSliders[i].isActive = false;
                dopesSliders[i].gameObject.SetActive(false);
            }
        }
    }
}
