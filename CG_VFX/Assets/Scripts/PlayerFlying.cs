using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerFlying : MonoBehaviour {

    [SerializeField] SteamVR_Behaviour_Pose pose;
    [SerializeField] Rigidbody vrRigidbody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SteamVR_Input._default.inActions.GrabPinch.GetState(pose.inputSource))
        {
            vrRigidbody.AddForce(transform.forward * 12f, ForceMode.Impulse);
        }
	}
}
