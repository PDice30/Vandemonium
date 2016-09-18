using UnityEngine;
using System.Collections;

public class EnemyCarMover : MonoBehaviour {

	// PUT THE OBJECT AS A PREFAB, USE THE SCENE CONTROLLER!!
	//public PlayerController playerController;
	private float carVelocity;
	private float sceneVelocity;
	//public GameObject sceneControllerObj;
	public SceneController sceneController;

	void Start () {
	//	sceneControllerObj = GameObject.Find ("SceneController");
	//	sceneController = sceneControllerObj.GetComponent<SceneController> ();
	}
	

	void Update () {
		//Add a check for the player's velocity

		transform.Translate (0, 0, -(Time.deltaTime * carVelocity * sceneController.SCENE_SPEED));
		if (transform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
			Destroy (gameObject);
		}
	}


	public void setVelocity(float velocity) {
		carVelocity = velocity;
	}

}
