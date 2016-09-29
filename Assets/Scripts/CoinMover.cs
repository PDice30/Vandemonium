using UnityEngine;
using System.Collections;

public class CoinMover : MonoBehaviour {

	public float coinVelocity;
	public float rotationSpeed;

	//public GameObject sceneControllerObj;
	public LevelSceneController levelSceneController;

	private Vector3 coinRotation;
	private Transform coinTransform;

	float yRotation;

	// Use this for initialization
	void Awake () {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
	}

	void Start () {
		//coinRotation = new Vector3 (0, rotationSpeed, 0);
		coinTransform = gameObject.transform;
		yRotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		coinTransform.Translate (0, 0, -(Time.deltaTime * coinVelocity * levelSceneController.SCENE_SPEED), Space.World);
		coinTransform.Rotate (2, 0, 0, Space.Self);
		//coinTransform.rotation = Quaternion.Euler(yRotation, 0, 0);
		//transform.rotation = Quaternion.Euler
		if (coinTransform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
			Destroy (gameObject);
		}
		yRotation += 1.0f;
	}


}
