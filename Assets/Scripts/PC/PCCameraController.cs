using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCCameraController : MonoBehaviour
{
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    public float lerpT = 0.5f;

    float currentX;
    float currentY;
    float targetX;
    float targetY;
    Quaternion originalRotation;

    void Update()
    {
        targetX += Input.GetAxis("Mouse X") * sensitivityX;
        targetY += Input.GetAxis("Mouse Y") * sensitivityY;
        targetX = Mathf.Clamp(targetX, minimumX, maximumX);
        targetY = Mathf.Clamp(targetY, minimumY, maximumY);
        currentX = Mathf.Lerp(currentX, targetX, lerpT);
        currentY = Mathf.Lerp(currentY, targetY, lerpT);
        Quaternion xRot = Quaternion.AngleAxis(currentX, Vector3.up);
        Quaternion yRot = Quaternion.AngleAxis(currentY, Vector3.left);

        transform.localRotation = originalRotation * xRot * yRot;
    }

    void Start()
    {
        originalRotation = transform.localRotation;
    }
}

/*
 public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
 */