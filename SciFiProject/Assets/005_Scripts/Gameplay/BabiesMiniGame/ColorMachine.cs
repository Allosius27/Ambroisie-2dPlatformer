using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachine : MonoBehaviour
{
    #region Fields

    private List<Color> listBaseRandomColors = new List<Color>();

    private Color currentColor;

    private float baseColorChangeTimer;

    #endregion

    #region Properties

    public bool canShoot { get; set; }

    public Color CurrentColor => currentColor;

    public List<Color> ListRandomColors => listRandomColors;

    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxColorBulletShot;

    [Space]

    [SerializeField] private GameObject colorSquare;

    [SerializeField] private float colorChangeTimer;

    [SerializeField] private List<Color> listRandomColors = new List<Color>();

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private int prestigePointsAtGained;
    [SerializeField] private int expJobAtGained;

    #endregion

    #region Behaviour

    private void Awake()
    {
        listBaseRandomColors.Clear();
        for (int i = 0; i < listRandomColors.Count; i++)
        {
            listBaseRandomColors.Add(listRandomColors[i]);
        }

        if (baseColorChangeTimer <= 0)
        {
            baseColorChangeTimer = colorChangeTimer;
        }

        canShoot = true;
    }

    private void Update()
    {
        if(GameCore.Instance.babiesFactoryMiniGameActive)
        {
            colorChangeTimer -= Time.deltaTime;

            if(colorChangeTimer <= 0)
            {
                colorChangeTimer = 0;
                SetColorSquare();
            }
        }
    }

    public void ChangeRandomColors(List<Color> newListColors)
    {
        listRandomColors.Clear();
        for (int i = 0; i < listBaseRandomColors.Count; i++)
        {
            listRandomColors.Add(listBaseRandomColors[i]);
        }
        for (int i = 0; i < newListColors.Count; i++)
        {
            listRandomColors.Add(newListColors[i]);
        }

        BabiesFactoryMiniGameManager.Instance.ColorsTouchs.SetColorsTouchs();
    }

    public void SetColorSquare()
    {
        colorChangeTimer = baseColorChangeTimer;

        int rnd = Random.Range(0, listRandomColors.Count);
        while(currentColor == listRandomColors[rnd])
        {
            rnd = Random.Range(0, listRandomColors.Count);
        }
        colorSquare.GetComponent<SpriteRenderer>().color = listRandomColors[rnd];
        currentColor = listRandomColors[rnd];
    }

    public IEnumerator DebugShoot()
    {
        yield return new WaitForSeconds(2f);

        canShoot = true;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;

            // shooting logic

            Debug.Log("Shoot");

            AllosiusDev.AudioManager.Play(sfxColorBulletShot.sound);

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
    }

    #endregion
}
