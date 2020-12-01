using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class SpeedBoost : MonoBehaviour , ICollectableItem
    {
        public void Boost()
        {
            var moons = FindObjectsOfType<MoonBehaviour>();
            foreach (var moon in moons)
            {
                if(moon.rotationSpeed < moon.maxRotationSpeed)
                {
                    moon.initialRotationSpeed += 1;
                    moon.rotationSpeed += 1;
                }
            }
            Destroy(gameObject);
        }
    }
}
