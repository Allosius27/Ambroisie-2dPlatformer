using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabButtonCtrl : ButtonCtrl
{
    private SettingsMenu settingsMenu;

    [SerializeField] private GameObject activeButton;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (settingsMenu.CurrentActiveTabSettingMenu != null && settingsMenu.CurrentActiveTabSettingMenu != this)
        {
            settingsMenu.CurrentActiveTabSettingMenu.SetActiveButton(false);
            settingsMenu.CurrentActiveTabSettingMenu.SetActiveMenuPanel(false);
        }

        settingsMenu.SetCurrentActiveTabSettingMenu(this);
        SetActiveButton(true);
        SetActiveMenuPanel(true);
    }

    public void SetActiveButton(bool _value)
    {
        activeButton.SetActive(_value);
    }

    public void SetActiveMenuPanel(bool _value)
    {
        menuPanel.SetActive(_value);
    }
}
