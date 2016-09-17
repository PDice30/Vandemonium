using UnityEngine;
using System.Collections;

public class Enemy_CarMovement : MonoBehaviour {



	private float _velocity;


	void Start () {
	}
	

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
