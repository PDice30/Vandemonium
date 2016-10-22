using UnityEngine;
using System.Collections;

public class MiscObjectMover : MonoBehaviour {

	private float objVelocity;
	private float sceneVelocity;
	public LevelSceneController levelSceneController;

	void Awake() {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
	}

	void Start () {
		
	}

	void Update () {
		//Add a check for the player's velocity
		transform.Translate (0, 0, -(Time.deltaTime * objVelocity * levelSceneController.SCENE_SPEED), Space.World);
		if (transform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
			Destroy (gameObject);
		}
	}

	public void setVelocity(float velocity) {
		objVelocity = velocity;
	}

}
