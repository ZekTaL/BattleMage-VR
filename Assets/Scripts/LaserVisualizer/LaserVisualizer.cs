using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class LaserVisualizer : MonoBehaviour
    {
        const float LineWidth = .1f;
        const float MaxRayDist = 100f;

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

            //Find closest object hit by laser
            RaycastHit closest;
            RaycastHit[] hits = Physics.RaycastAll(startPoint, start.forward, MaxRayDist, validHitLayers);
            bool hasHit = false; ;
            if (hits.Length > 0)
            {
                hasHit = true;
                closest = hits[0];
                if (hits.Length > 1)
                {
                    float closestDist = hits[0].distance;
                    for (int i = 1; i < hits.Length; i++)
                    {
                        if (hits[i].distance < closestDist)
                        {
                            closestDist = hits[i].distance;
                            closest = hits[i];
                        }
                    }
                }

                SetColor(hasHit);
                UpdateScalePos(closest.point, hasHit);
            }

            //    Debug.DrawLine(closest.point, closest.point + Vector3.up, Color.red, 3f);
        }


        public void TurnOffLaser()
        {
            ToggleVisibility(false);
        }

        void UpdateScalePos(Vector3 hitPoint, bool didHit)
        {
            Vector3 end = didHit ? hitPoint : startPoint + start.forward * 100f;
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