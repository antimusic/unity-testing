using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource thrustAudio;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		thrustAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
		{
			print ("Thrusting!");
			rigidBody.AddRelativeForce(Vector3.up);
			if (!thrustAudio.isPlaying)
			{
			thrustAudio.Play();
			}
			else
			{
				thrustAudio.Stop();
			}
		}
		if (Input.GetKey(KeyCode.A))
		{
			print ("rotating left");
			transform.Rotate(Vector3.forward);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			print ("rotating right");
			transform.Rotate(-Vector3.forward);
		} 
	} 
}
  