using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public List<GameObject> drops;

    public GameObject Drop()
    {
        int random = UnityEngine.Random.Range(0, 2);
        return drops[random];
    }
}
