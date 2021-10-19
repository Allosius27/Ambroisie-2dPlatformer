using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopeSlider : MonoBehaviour
{
    #region Fields

    private Slider slider;

    #endregion

    #region UnityInspector

    [SerializeField] private float speed;

    #endregion

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCore.Instance.dopesMiniGameActive && slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime * speed ;
        }
    }

    public Slider GetSlider()
    {
        return slider;
    }
}
