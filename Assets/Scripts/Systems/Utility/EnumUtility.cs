using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumUtility
{
    public static int GetLength<T>() where T : System.Enum
    {
        return System.Enum.GetNames(typeof(T)).Length;
    }

    public static T RandEnum<T>() where T : System.Enum
    {
        int rand = Random.Range(0, System.Enum.GetNames(typeof(T)).Length);
        T randEnum = (T)(object)rand;
        return randEnum;
    }
}
