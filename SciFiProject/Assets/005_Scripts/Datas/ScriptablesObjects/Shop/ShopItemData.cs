using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItemData", menuName = "ShopItemData", order = 0)]
public class ShopItemData : ScriptableObject
{
    public Sprite sprite;
    public int costItem;
}
