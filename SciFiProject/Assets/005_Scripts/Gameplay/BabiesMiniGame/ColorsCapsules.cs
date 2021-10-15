using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsCapsules : MonoBehaviour
{
    #region Fields

    private List<Color> baseColorsCapsulesColors = new List<Color>();

    #endregion

    #region Properties

    public int currentIndexColorCapsule { get; set; }

    public List<GameObject> ListColorsCapsules => listColorsCapsules;

    #endregion

    #region UnityInspector

    [SerializeField] private List<GameObject> listColorsCapsules = new List<GameObject>();

    #endregion

    #region Behaviour

    private void Awake()
    {
        InitBasesCapsulesColors();
    }

    public void InitBasesCapsulesColors()
    {
        for (int i = 0; i < listColorsCapsules.Count; i++)
        {
            baseColorsCapsulesColors.Add(listColorsCapsules[i].GetComponent<SpriteRenderer>().color);
        }
    }

    public void ReinitColorsCapsulesColors()
    {
        for (int i = 0; i < listColorsCapsules.Count; i++)
        {
            listColorsCapsules[i].GetComponent<SpriteRenderer>().color = baseColorsCapsulesColors[i];
        }
    }

    #endregion
}
