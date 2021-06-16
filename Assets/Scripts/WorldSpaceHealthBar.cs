using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the Healthbar of the enemies
    /// </summary>
    public class WorldSpaceHealthBar : MonoBehaviour
    {
        public Image BarFG;
        public Gradient gradient;

        Transform cam;

        /// <summary>
        /// Fill the bar to the health amount
        /// </summary>
        /// <param name="perc">percentage to fill</param>
        public void SetPercentage(float perc)
        {
            BarFG.fillAmount = perc;
            BarFG.color = gradient.Evaluate(perc);
        }

        void Start()
        {
            cam = RigManager.MainCamera.transform;
        }

        void Update()
        {
            transform.LookAt(cam);
            //transform.Rotate(0, 180, 0); //Or you can just set the fill origin to right. It's a bug with the camera
        }
    }
}
