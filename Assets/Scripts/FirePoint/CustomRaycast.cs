using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Static class that manages when using a raycast
    /// </summary>
    public static class CustomRaycast
    {
        /// <summary>
        /// Function that send all the raycast to check what to hit
        /// </summary>
        /// <param name="startPoint">start position of the raycast</param>
        /// <param name="forward">direction of the raycast</param>
        /// <param name="distance">maximum distance of the raycast</param>
        /// <param name="validLayers">layer mask of teh raycast</param>
        /// <param name="hitPoint">if there's one, hit point of the raycast</param>
        public static bool RaycastAll(Vector3 startPoint, Vector3 forward, float distance, LayerMask validLayers, out Vector3 hitPoint)
        {
            RaycastHit[] hits = Physics.RaycastAll(startPoint, forward, distance, validLayers);
            hitPoint = Vector3.zero;

            // if it hits something
            if (hits.Length > 0)
            {
                // if its only 1, get it
                int indexClosestDist = 0;
                hitPoint = hits[0].point;
                
                // if more then 1, check the closest
                if (hits.Length > 1)
                {
                    float closestDist = hits[0].distance;

                    for (int i = 1; i < hits.Length; i++)
                    {
                        if (hits[i].distance < closestDist)
                        {
                            closestDist = hits[i].distance;
                            hitPoint = hits[i].point;
                            indexClosestDist = i;
                        }
                    }
                }
                
                return !hits[indexClosestDist].collider.gameObject.CompareTag("Ground");

            }

            return false;
        }
    }
}