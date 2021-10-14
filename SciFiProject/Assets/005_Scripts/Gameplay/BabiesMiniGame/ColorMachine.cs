using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachine : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private GameObject colorSquare;

    [SerializeField] private List<Color> listRandomColors = new List<Color>();

    #endregion

    #region Behaviour

    public void SetColorSquare()
    {
        int rnd = Random.Range(0, listRandomColors.Count);
        colorSquare.GetComponent<SpriteRenderer>().color = listRandomColors[rnd];
    }

    #endregion
}
