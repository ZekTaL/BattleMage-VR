﻿using UnityEngine;

namespace BattleMage
{
    public static class CustomRaycast
    {
        public static bool RaycastAll(Vector3 startPoint, Vector3 forward, float distance, LayerMask validLayers, out Vector3 hitPoint)
        {
            RaycastHit[] hits = Physics.RaycastAll(startPoint, forward, distance, validLayers);
            hitPoint = Vector3.zero;

            if (hits.Length > 0)
            {
                int indexClosestDist = 0;
                hitPoint = hits[0].point;
                
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