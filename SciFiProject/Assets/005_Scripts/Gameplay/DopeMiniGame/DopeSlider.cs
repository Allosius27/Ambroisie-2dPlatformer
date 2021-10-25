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

    public bool isActive { get; set; }

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
    [SerializeField] private Image colorBackground;

    [SerializeField] private int expPointsGainedMultiplier;
    [SerializeField] private int prestigePointsGainedMultiplier;

    [SerializeField] private Sprite handleSprite_01, handleSprite_02, handleSprite_03, handleSprite_04;
    [SerializeField] private Color sliderColor_01, sliderColor_02, sliderColor_03, sliderColor_04;

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
        if(slider.value <= slider.maxValue / 4)
        {
            handleImage.sprite = handleSprite_01;
            colorBackground.color = sliderColor_01;
        }
        else if(slider.value <= slider.maxValue / 2)
        {
            handleImage.sprite = handleSprite_02;
            colorBackground.color = sliderColor_02;
        }
        else if (slider.value <= slider.maxValue / 1.3f)
        {
            handleImage.sprite = handleSprite_03;
            colorBackground.color = sliderColor_03;
        }
        else
        {
            handleImage.sprite = handleSprite_04;
            colorBackground.color = sliderColor_04;

        }

        if(GameCore.Instance.dopesMiniGameActive && slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime * speed ;

            
        }

        if (slider.value >= slider.maxValue && sliderEmpty == false)
        {
            slider.interactable = false;
            handleImage.color = Color.white;
            eliminateImage.enabled = true;
            sliderEmpty = true;

            for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
            {
                if (GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].sliderEmpty == false &&
                    GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].isActive)
                {
                    return;
                }
            }

            StartCoroutine(DopeMiniGameManager.Instance.EndDopeMiniGame());
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
