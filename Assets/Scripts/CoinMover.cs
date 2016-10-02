using UnityEngine;
using System.Collections;

public class CoinMover : MonoBehaviour {

	public float coinVelocity;
	public float rotationSpeed;

	//public GameObject sceneControllerObj;
	public LevelSceneController levelSceneController;

	private Vector3 coinRotation;
	private Transform coinTransform;

	public bool hasBeenCollected;

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
		if (!hasBeenCollected) {
			coinTransform.Translate (0, 0, -(Time.deltaTime * coinVelocity * levelSceneController.SCENE_SPEED), Space.World);
			coinTransform.Rotate (60 * Time.deltaTime, 0, 0, Space.Self);
		} else {
			coinTransform.Translate (0, 4f * Time.deltaTime * coinVelocity, 0, Space.World);
			coinTransform.Rotate (900 * Time.deltaTime, 0, 0, Space.Self);
			//Probably bad to call new so much here
			coinTransform.localScale = 
				new Vector3(coinTransform.localScale.x - (1.25f * Time.deltaTime),
				coinTransform.localScale.y,
				coinTransform.localScale.z - (1.25f * Time.deltaTime));
			if (coinTransform.localScale.x <= 0.5f) {
				Destroy (gameObject);
			}
		}
			
		//coinTransform.rotation = Quaternion.Euler(yRotation, 0, 0);
		//transform.rotation = Quaternion.Euler
		if (coinTransform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
			Destroy (gameObject);
		}
		yRotation += 1.0f;
	}


}
