using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class LaserVisualizer : MonoBehaviour
    {
        const float LineWidth = .1f;

        [SerializeField] Color colorInvalid = Color.red;
        [SerializeField] Color colorValid = Color.green;
        [SerializeField] Transform start;
        [SerializeField] LayerMask validHitLayers;
        [SerializeField] GameObject cubePrimitive;

        //Status
        bool currentlyVisible;

        //Reference
        Transform line;
        Renderer lineRenderer;

        void Start()
        {
            //Cache references
            line = cubePrimitive.transform;
            lineRenderer = cubePrimitive.GetComponent<Renderer>();

            //Initialize
            SetColor(false);
            line.gameObject.SetActive(false);
        }

        public void UpdateLaser()
        {
            ToggleVisibility(true);

            bool hasHit = (Physics.Raycast(startPoint, start.forward, out RaycastHit hit, validHitLayers));
            SetColor(hasHit);
            UpdateScalePos(hit, hasHit);
            if (hasHit)
            {
                Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.red, 3f);
            }
        }

        public void TurnOffLaser()
        {
            ToggleVisibility(false);
        }

        void UpdateScalePos(RaycastHit hit, bool didHit)
        {
            Vector3 end = didHit ? hit.point : startPoint + start.forward * 100f;
            Vector3 dir = end - startPoint;
            float dist = dir.magnitude;

            line.position = Vector3.Lerp(startPoint, end, 0.5f); //Midpoint
            line.rotation = Quaternion.LookRotation(dir);
            line.localScale = new Vector3(LineWidth, LineWidth, dist);
        }

        #region minor methods
        Vector3 startPoint => start.position;
        void SetStartTransform(Transform startPoint) => this.start = startPoint;
        void ToggleVisibility(bool isVisible)
        {
            if (isVisible != currentlyVisible)
            {
                currentlyVisible = isVisible;
                line.gameObject.SetActive(isVisible);
            }
        }
        void SetColor(bool isValidColor)
        {
            lineRenderer.material.color = isValidColor ? colorValid : colorInvalid;
        }

        
        #endregion
    }
}