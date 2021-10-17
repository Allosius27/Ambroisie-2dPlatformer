using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachine : MonoBehaviour
{
    #region Fields

    private Color currentColor;

    #endregion

    #region Properties

    public Color CurrentColor => currentColor;

    public List<Color> ListRandomColors => listRandomColors;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject colorSquare;

    [SerializeField] private List<Color> listRandomColors = new List<Color>();

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private int prestigePointsAtGained;
    [SerializeField] private int expJobAtGained;

    #endregion

    #region Behaviour

    public void SetColorSquare()
    {
        int rnd = Random.Range(0, listRandomColors.Count);
        while(currentColor == listRandomColors[rnd])
        {
            rnd = Random.Range(0, listRandomColors.Count);
        }
        colorSquare.GetComponent<SpriteRenderer>().color = listRandomColors[rnd];
        currentColor = listRandomColors[rnd];
    }

    public void Shoot()
    {
        // shooting logic

        Debug.Log("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(firePoint);
        bullet.transform.localPosition = Vector3.zero;


        bullet.GetComponent<ColorMachineBullet>().direction = new Vector3(0, -1, 0);
        bullet.GetComponent<ColorMachineBullet>().speed = bulletSpeed;
        bullet.GetComponent<ColorMachineBullet>().bulletColor = currentColor;
        bullet.GetComponent<ColorMachineBullet>().ExpJobAtGained = expJobAtGained;
        bullet.GetComponent<ColorMachineBullet>().PrestigePointsAtGained = prestigePointsAtGained;

        bullet.GetComponent<SpriteRenderer>().color = currentColor;
    }

    #endregion
}
