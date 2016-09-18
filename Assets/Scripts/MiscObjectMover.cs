using UnityEngine;
using System.Collections;

public class MiscObjectMover : MonoBehaviour {


	//This is probably bad, figure out a way to broadcast a message.
	private float objVelocity;
	//public GameObject sceneControllerObj;
	public SceneController sceneController;

	void Awake () {
	//	sceneController = sceneControllerObj.GetComponent<SceneController> ();
	}
	void Start () {
		
	}

	void Update () {
		//Add a check for the player's velocity
		transform.Translate (0, 0, -(Time.deltaTime * objVelocity * sceneController.SCENE_SPEED));
		if (transform.position.z < -20) {
			Destroy (gameObject);
		}
	}

	public void setVelocity(float velocity) {
		objVelocity = velocity;
	}

}
