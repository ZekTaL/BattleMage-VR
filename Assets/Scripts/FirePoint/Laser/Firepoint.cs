using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    //References the firing position of the rig
    public class Firepoint : MonoBehaviour
    {
        const float LineWidth = 0.1f;
        const float MaxRayDist = 100f;

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
            laserTransform.gameObject.SetActive(false);
        }

        void Update()
        {
            if (!isOn)
                return;

            //Find closest object hit by laser
            if (RaycastShooter.Shoot(startPoint, transform.forward, MaxRayDist, raycastLayers, out Vector3 hitPoint))
            {
                HasHit = true;
                HitPosition = hitPoint;
            }
            else
            {
                HasHit = false;
                HitPosition = startPoint + transform.forward * MaxRayDist;
            }

            SetColor(HasHit);
            UpdateScalePos(startPoint, HitPosition);
        }

        public void ToggleLaser(bool isOn)
        {
            ToggleOn(isOn);
            if (isOn)
                Update();
        }

        void UpdateScalePos(Vector3 startPoint, Vector3 end)
        {
            Vector3 dir = end - startPoint;
            float dist = dir.magnitude;
            Debug.DrawLine(startPoint, end, Color.red);

            laserTransform.position = Vector3.Lerp(startPoint, end, 0.5f); //Midpoint
            laserTransform.rotation = Quaternion.LookRotation(dir);
            laserTransform.localScale = new Vector3(LineWidth, LineWidth, dist);
        }

        #region minor methods
        void ToggleOn(bool isOn)
        {
            this.isOn = isOn;
            Debug.Log("laserTransform " + (laserTransform == null));
            Debug.Log("laserTransform.gameObject " + (laserTransform.gameObject == null));
            laserTransform.gameObject.SetActive(isOn);
        }

        void SetColor(bool isValidColor)
        {
            laserRenderer.material.color = isValidColor ? settings.ColorValid : settings.ColorInvalid;
        }
        #endregion
    }
}