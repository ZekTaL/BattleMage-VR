//using System.Collections;
//using UnityEngine;

//namespace Assets.Scripts
//{
//    /// <summary>
//    /// Class that checks which layer is hit by the raycast
//    /// </summary>
//    public class LayermaskChecker : MonoBehaviour
//    {
//        //<referencs layer mask 
//        public LayerMask groundLayer;
//        public LayerMask enemyLayer;

//        void Update()
//        {
//            Debug.Log("I USE THIS CLASS");

//            // Shoot with the LeftMouseClick
//            if (Input.GetMouseButtonDown(0))
//            {
//                Vector3 mousePos = Input.mousePosition;
//                mousePos.z = 10f;
//                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                Physics.Raycast(ray, out RaycastHit hit, int.MaxValue);
//                Debug.DrawRay(ray.origin, ray.direction, Color.red, 2f);

//                if (hit.collider != null)
//                {
//                    Debug.Log("s");

//                    //Debug.Log("groundLayer: " + groundLayer + ", hit.collider " + hit.collider);

//                    if (HitsGround(hit.collider))
//                    {
//                        Debug.Log("Hits grounds");
//                    }
//                    else if (HitsEnemy(hit.collider))
//                    {
//                        Debug.Log("Hits Enemy");
//                    }
//                }
//            }
//        }

//        //use bit shift to check for layers
//        bool HitsGround(Collider col) => groundLayer == (groundLayer | 1 << col.gameObject.layer);
//        bool HitsEnemy(Collider col) => enemyLayer == (enemyLayer | 1 << col.gameObject.layer);

//    }
//}