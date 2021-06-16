using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that manages the camera controller of the player on PC
/// </summary>
public class PCCameraController : MonoBehaviour
{
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    public float lerpT = 0.5f;

    float currentX;
    float currentY;
    float targetX;
    float targetY;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        targetX += Input.GetAxis("Mouse X") * sensitivityX;
        targetY += Input.GetAxis("Mouse Y") * sensitivityY;
        targetY = Mathf.Clamp(targetY, minimumY, maximumY);
        //currentX = Mathf.Lerp(currentX, targetX, lerpT);
        //currentY = Mathf.Lerp(currentY, targetY, lerpT);
        Quaternion xRot = Quaternion.AngleAxis(targetX, Vector3.up);
        Quaternion yRot = Quaternion.AngleAxis(targetY, Vector3.left);

        transform.localRotation = originalRotation * xRot * yRot;
    }

}