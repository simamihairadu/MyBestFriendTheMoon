using Assets.Scripts.Collectables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSizeBoost : MonoBehaviour, ICollectableItem
{
    public void Boost()
    {
        var moons = FindObjectsOfType<MoonBehaviour>();
        foreach (var moon in moons)
        {
            if (moon.transform.localScale.x < 0.1f)
            {
                moon.transform.localScale += new Vector3(0.02f, 0.02f, 0.0f);
            }
        }
        Destroy(gameObject);
    }
}
