using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class VrController : MonoBehaviour
{
    public Vector3 Velocity { get { return pose.GetVelocity(); } }
    public Vector3 AngularVelocity { get { return pose.GetAngularVelocity(); } }

    public SteamVR_Input_Sources inputSource;

    private SteamVR_Behaviour_Pose pose;

    // Start is called before the first frame update
    void Start()
    {
        pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
