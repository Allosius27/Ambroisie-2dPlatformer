using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Type
    {
        Default,
        Player,
        Ground,
    }
    public Type type;
}
