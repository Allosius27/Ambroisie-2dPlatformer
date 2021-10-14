using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsTouchs : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private List<GameObject> listColorsTouchs = new List<GameObject>();

    #endregion

    #region Behaviour

    public void ActiveColorsTouchs(bool value)
    {
        for (int i = 0; i < listColorsTouchs.Count; i++)
        {
            listColorsTouchs[i].SetActive(false);
        }
    }

    public void SetColorsTouchs()
    {
        List<Color> randomColors = BabiesFactoryMiniGameManager.Instance.ColorMachine.GetComponent<ColorMachine>().ListRandomColors;
        for (int i = 0; i < randomColors.Count; i++)
        {
            listColorsTouchs[i].SetActive(true);
            listColorsTouchs[i].GetComponent<SpriteRenderer>().color = randomColors[i];
        }
    }

    #endregion
}
