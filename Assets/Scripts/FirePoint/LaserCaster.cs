using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Firing position of the rig. 
    /// It's a laser that can be activated so you can see where you're shooting
    /// </summary>
    public class LaserCaster : MonoBehaviour
    {
        // width of the laser
        const float LineWidth = 0.1f;
        // maximum distance of the raycast
        const float MaxRayDist = 35f;

        [SerializeField] LayerMask raycastLayers;
        [SerializeField] GameObject cubePrimitive;

        //Status
        bool isOn;

        //Reference
        Transform laserTransform;
        Renderer laserRenderer;
        GameSettings settings;

        //Prop
        public bool HasHit { get; private set; }
        public Vector3 HitPosition { get; private set; }
        Vector3 startPoint => transform.position;

        void Start()
        {
            //Cache references
            laserTransform = cubePrimitive.transform;
            laserRenderer = cubePrimitive.GetComponent<Renderer>();
            settings = GameSettings.Instance;

            //Initialize
            SetColor(false);
            // keep the laser always on
            laserTransform.gameObject.SetActive(true);
        }

        void Update()
        {
            // if the laser is off, don't show it
            if (!isOn)
                return;

            //Find closest object hit by laser
            if (CustomRaycast.RaycastAll(startPoint, transform.forward, MaxRayDist, raycastLayers, out Vector3 hitPoint))
            {
                HasHit = true;
                HitPosition = hitPoint;
            }
            else
            {
                HasHit = false;
                HitPosition = startPoint + transform.forward * MaxRayDist;
            }

            // Set the color of the laser
            SetColor(HasHit);
            // Update the scale of the laser
            UpdateScalePos(startPoint, HitPosition);
        }

        /// <summary>
        /// Toggle the laser on and off
        /// </summary>
        public void ToggleLaser(bool isOn)
        {
            ToggleOn(isOn);
            if (isOn)
                Update();
        }

        /// <summary>
        /// Update the scale of the laser based on the raycast hit position
        /// </summary>
        /// <param name="startPoint">start position of the laser</param>
        /// <param name="end">end position of the laser</param>
        void UpdateScalePos(Vector3 startPoint, Vector3 end)
        {
            Vector3 dir = end - startPoint;
            float dist = dir.magnitude;
            //Debug.DrawLine(startPoint, end, Color.red);

            laserTransform.position = Vector3.Lerp(startPoint, end, 0.5f); //Midpoint
            laserTransform.rotation = Quaternion.LookRotation(dir);
            laserTransform.localScale = new Vector3(LineWidth, LineWidth, dist);
        }

        #region minor methods

        /// <summary>
        /// Set active or inactive the laser gameobject
        /// </summary>
        void ToggleOn(bool isOn)
        {
            this.isOn = isOn;
            laserTransform.gameObject.SetActive(isOn);
        }

        /// <summary>
        /// Update the color of the laser if it's a valid or invalid hit
        /// </summary>
        /// <param name="isValidColor"></param>
        void SetColor(bool isValidColor)
        {
            laserRenderer.material.color = isValidColor ? settings.ColorValid : settings.ColorInvalid;
        }

        #endregion
    }
}