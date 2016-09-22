using UnityEngine;
using System.Collections;

public class MiscObjectMover : MonoBehaviour {

	private float objVelocity;
	private float sceneVelocity;
	public SceneController sceneController;

	void Start () {
		
	}

	void Update () {
		//Add a check for the player's velocity
		transform.Translate (0, 0, -(Time.deltaTime * objVelocity * sceneController.SCENE_SPEED));
		if (transform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
			Destroy (gameObject);
		}
	}

	public void setVelocity(float velocity) {
		objVelocity = velocity;
	}

}
