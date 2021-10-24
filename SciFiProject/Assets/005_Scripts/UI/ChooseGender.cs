using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGender : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private GameObject chooseGenderMenu;

    #endregion

    #region Behaviour

    private void Start()
    {
        ActiveChooseGenderMenu(true);
    }

    public void ActiveChooseGenderMenu(bool value)
    {
        chooseGenderMenu.SetActive(value);

        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        GameCore.Instance.PlayerStats.GetComponent<Player>().canControl = !value;

    }

    public void SetGenderPlayer(bool _isFemale)
    {
        GameCore.Instance.PlayerStats.GetComponent<Player>().isFemale = _isFemale;
        GameCore.Instance.PlayerStats.GetComponent<Player>().SetGender();

        ActiveChooseGenderMenu(false);
    }

    #endregion

}
