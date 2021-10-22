using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopeSlider : MonoBehaviour
{
    #region Fields

    private Slider slider;

    private Color baseHandleImageColor;

    #endregion

    #region Properties

    public Slider Slider => slider;

    public int ExpPointsGainedMultiplier => expPointsGainedMultiplier;
    public int PrestigePointsGainedMultiplier => prestigePointsGainedMultiplier;

    public Color BaseHandleImageColor => baseHandleImageColor;

    public Image HandleImage => handleImage;

    public Image EliminateImage => eliminateImage;

    public bool sliderEmpty { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private float speed;
    [SerializeField] private float multiplierSpeedPerLevel = 1.0f;

    [SerializeField] private Image handleImage;
    [SerializeField] private Image eliminateImage;

    [SerializeField] private int expPointsGainedMultiplier;
    [SerializeField] private int prestigePointsGainedMultiplier;

    #endregion

    private void Awake()
    {
        slider = GetComponent<Slider>();

        sliderEmpty = false;

        baseHandleImageColor = handleImage.color;

        eliminateImage.enabled = false;

        slider.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCore.Instance.dopesMiniGameActive && slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime * speed ;

            if(slider.value >= slider.maxValue)
            {
                slider.interactable = false;
                handleImage.color = Color.white;
                eliminateImage.enabled = true;
                sliderEmpty = true;

                for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
                {
                    if(GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].sliderEmpty == false)
                    {
                        return;
                    }
                }

                StartCoroutine(DopeMiniGameManager.Instance.EndDopeMiniGame());
            }
        }
    }

    public void ReinitValues()
    {
        slider.value = 0;
        sliderEmpty = false;
        eliminateImage.enabled = false;
        handleImage.color = baseHandleImageColor;

        slider.interactable = true;
    }

    public void ChangeSpeed()
    {
        this.speed *= multiplierSpeedPerLevel;
    }

    public Slider GetSlider()
    {
        return slider;
    }
}
