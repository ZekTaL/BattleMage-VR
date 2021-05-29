using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class LaserVisualizer : MonoBehaviour
    {
        const float LineWidth = 0.1f;
        const float MaxRayDist = 100f;

        
        [SerializeField] GameObject cubePrimitive;

        //Status
        bool isVisible;

        //Reference
        Transform cube;
        Renderer cubeRenderer;
        RigManager rigM;

        //Cache
        LayerMask raycastLayers;

        //Prop
        public bool HasHit { get; private set; }
        public Vector3 HitPosition { get; private set; }
        Vector3 startPoint => transform.position;

        void Start()
        {
            //Cache references
            cube = cubePrimitive.transform;
            cubeRenderer = cubePrimitive.GetComponent<Renderer>();
            rigM = RigManager.Instance;
            raycastLayers = rigM.RaycastLayers;
            
            //Initialize
            SetColor(false);
            cube.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!isVisible)
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

        public void EnableLaser()
        {
            ToggleVisibility(true);
            Update();
        }

        public void DisableLaser() => ToggleVisibility(false);

        void UpdateScalePos(Vector3 startPoint, Vector3 end)
        {
            Vector3 dir = end - startPoint;
            float dist = dir.magnitude;
            Debug.DrawLine(startPoint, end, Color.red);

            cube.position = Vector3.Lerp(startPoint, end, 0.5f); //Midpoint
            cube.rotation = Quaternion.LookRotation(dir);
            cube.localScale = new Vector3(LineWidth, LineWidth, dist);
        }

        #region minor methods
        void ToggleVisibility(bool isVisible)
        {
            this.isVisible = isVisible;
            cube.gameObject.SetActive(isVisible);
        }

        void SetColor(bool isValidColor)
        {
            cubeRenderer.material.color = isValidColor ? rigM.ColorValid: rigM.ColorInvalid;
        }
        #endregion
    }
}