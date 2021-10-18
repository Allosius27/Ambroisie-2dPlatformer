using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorTouchCtrl : MonoBehaviour, IPointerDownHandler
{
    #region Properties

    public Color currentColor { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxColorButtonClicked;

    #endregion

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down " + gameObject.name);
        AllosiusDev.AudioManager.Play(sfxColorButtonClicked.sound);
        for (int i = 0; i < BabiesFactoryMiniGameManager.Instance.NumberOfColorsMachinesActived; i++)
        {
            if (i < BabiesFactoryMiniGameManager.Instance.ColorsMachines.Count && currentColor == BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].CurrentColor)
            {
                Debug.Log("color equals");
                BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].Shoot();
            }
            else
            {
                Debug.Log("not color equals");
            }
        }
    }

    /*private void OnMouseDown()
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
    }*/
}
