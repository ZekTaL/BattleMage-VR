//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace BattleMage
//{
//    public class LaserVisualizer : MonoBehaviour
//    {
//        const float LineWidth = 0.1f;
//        const float MaxRayDist = 100f;

//        [SerializeField] Color colorInvalid = Color.red;
//        [SerializeField] Color colorValid = Color.green;
//        [SerializeField] LayerMask validHitLayers;
//        [SerializeField] GameObject cubePrimitive;

//        //Status
//        bool isActive;
        
//        //Reference
//        Transform cube;
//        Renderer cubeRenderer;

//        //Prop
//        Vector3 startPoint => transform.position;

//        void Start()
//        {
//            //Cache references
//            cube = cubePrimitive.transform;
//            cubeRenderer = cubePrimitive.GetComponent<Renderer>();

//            //Initialize
//            SetColor(false);
//            cube.gameObject.SetActive(false);
//        }

//        private void Update()
//        {
//            if (!isActive)
//                return;

//            //Find closest object hit by laser
//            RaycastHit closest;
//            RaycastHit[] hits = Physics.RaycastAll(startPoint, transform.forward, MaxRayDist, validHitLayers);
//            if (hits.Length > 0)
//            {
//                closest = hits[0];
//                if (hits.Length > 1)
//                {
//                    float closestDist = hits[0].distance;
//                    for (int i = 1; i < hits.Length; i++)
//                    {
//                        if (hits[i].distance < closestDist)
//                        {
//                            closestDist = hits[i].distance;
//                            closest = hits[i];
//                        }
//                    }
//                }

//                SetColor(true);
//                UpdateScalePos(startPoint, closest.point);
//            }
//            else //Missing
//            {
//                SetColor(false);
//                UpdateScalePos(startPoint, startPoint + transform.forward * MaxRayDist);
//            }
//        }

//        public void EnableLaser() => ToggleVisibility(true);
//        public void DisableLaser() => ToggleVisibility(false);

//        void UpdateScalePos(Vector3 startPoint, Vector3 end)
//        {
//            Vector3 dir = end - startPoint;
//            float dist = dir.magnitude;
//            Debug.DrawLine(startPoint, end, Color.red);

//            cube.position = Vector3.Lerp(startPoint, end, 0.5f); //Midpoint
//            cube.rotation = Quaternion.LookRotation(dir);
//            cube.localScale = new Vector3(LineWidth, LineWidth, dist);
//        }

//        #region minor methods
//        void ToggleVisibility(bool isActive)
//        {
//            if (this.isActive != isActive)
//            {
//                this.isActive = isActive;
//                cube.gameObject.SetActive(isActive);
//            }
//        }

//        void SetColor(bool isValidColor)
//        {
//            cubeRenderer.material.color = isValidColor ? colorValid : colorInvalid;
//        }
//        #endregion
//    }
//}