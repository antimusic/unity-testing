using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {


[SerializeField] Vector3 movementVector; //todo remove from inspector later
[SerializeField] float period = 2f;
[Range (0,1)] [SerializeField] float movementFactor; //0 from not moved, 1 from fully moved

Vector3 startingPos;
	
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//safeguard if period is zero
		if (period <= Mathf.Epsilon) { return; } //mathf.epsilon is the smallest float, closest to zero, used for accuracy
		float cycles = Time.time / period; // grows continually from 0
		
		const float tau = Mathf.PI * 2f; // pi times two, about 6.28
		float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to +1
		print(movementFactor);
		
		movementFactor = rawSinWave / 2f + 0.5f;
		
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
		
	}
}
