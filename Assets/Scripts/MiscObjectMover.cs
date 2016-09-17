using UnityEngine;
using System.Collections;

public class MiscObjectMover : MonoBehaviour {


	private float _velocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Add a check for the player's velocity
		transform.Translate (0, 0, -(Time.deltaTime * _velocity));
		if (transform.position.z < -20) {
			Destroy (gameObject);
		}
	}

	public void setVelocity(float velocity) {
		_velocity = velocity;
	}

}
