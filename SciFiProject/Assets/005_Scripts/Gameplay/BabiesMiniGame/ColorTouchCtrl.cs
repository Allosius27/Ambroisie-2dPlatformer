using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTouchCtrl : MonoBehaviour
{
    #region Properties

    public Color currentColor { get; set; }

    #endregion

    private void OnMouseDown()
    {
        Debug.Log("Mouse Collides ColorTouch " + gameObject.name);

        if (currentColor == BabiesFactoryMiniGameManager.Instance.ColorMachine.CurrentColor)
        {
            Debug.Log("color equals");
            BabiesFactoryMiniGameManager.Instance.ColorMachine.Shoot();
        }
        else
        {
            Debug.Log("not color equals");
        }
    }
}
