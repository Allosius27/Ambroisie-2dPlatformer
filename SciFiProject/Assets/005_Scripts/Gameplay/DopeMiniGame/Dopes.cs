using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dopes : MonoBehaviour
{
    #region Properties

    public List<DopeSlider> DopesSliders => dopesSliders;

    #endregion

    #region UnityInspector

    [SerializeField] private List<DopeSlider> dopesSliders = new List<DopeSlider>();

    #endregion
}
