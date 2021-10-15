using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsTouchs : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private List<ColorTouchCtrl> listColorsTouchs = new List<ColorTouchCtrl>();

    #endregion

    #region Behaviour

    public void ActiveColorsTouchs(bool value)
    {
        for (int i = 0; i < listColorsTouchs.Count; i++)
        {
            listColorsTouchs[i].gameObject.SetActive(false);
        }
    }

    public void SetColorsTouchs()
    {
        List<Color> randomColors = BabiesFactoryMiniGameManager.Instance.ColorMachine.ListRandomColors;
        for (int i = 0; i < randomColors.Count; i++)
        {
            listColorsTouchs[i].gameObject.SetActive(true);
            listColorsTouchs[i].GetComponent<SpriteRenderer>().color = randomColors[i];
            listColorsTouchs[i].currentColor = randomColors[i];
        }
    }

    #endregion
}
