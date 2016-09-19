using UnityEngine;
using System.Collections;

public class EnemyCarMover : MonoBehaviour {

	// PUT THE OBJECT AS A PREFAB, USE THE SCENE CONTROLLER!!
	//public PlayerController playerController;
	private float carVelocity;
	private float sceneVelocity;
	//public GameObject sceneControllerObj;
	public SceneController sceneController;

	private bool isMarkedForDestroy = false;

	private Rigidbody carRigidbody;

	void Awake() {
		carRigidbody = gameObject.GetComponent<Rigidbody> ();
	}

	void Start () {
	//	sceneControllerObj = GameObject.Find ("SceneController");
	//	sceneController = sceneControllerObj.GetComponent<SceneController> ();
	}
	

	void Update () {
		//Add a check for the player's velocity
		//if (!isMarkedForDestroy) {
			transform.Translate (0, 0, -(Time.deltaTime * carVelocity * sceneController.SCENE_SPEED), Space.World);
			if (transform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
				Destroy (gameObject);
			}
		//} 


	}


	public void setVelocity(float velocity) {
		carVelocity = velocity;
	}

	public void markForDestroy(int direction) {
		isMarkedForDestroy = true;
		carRigidbody.useGravity = true;
		carRigidbody.isKinematic = false;
		StartCoroutine (moveAndDestroy(direction));
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("EnemyCar")) {
			/**********/
			/* TODO: if (playerPowerup Actived to make it so they all collide!)
			/*********/
			Rigidbody collRigidbody = coll.gameObject.GetComponent<Rigidbody> ();
			if (carRigidbody.isKinematic && collRigidbody.isKinematic) {
				//Do Nothing
			} else if (!carRigidbody.isKinematic && collRigidbody.isKinematic) { // Coll
				coll.gameObject.GetComponent<EnemyCarMover> ().markForDestroy (Random.Range (0, 2));
			} else if (carRigidbody.isKinematic && !collRigidbody.isKinematic) { // this
				markForDestroy(Random.Range(0, 2));
			} else { //Both are marked for destroy
			
			}


		}

	}


	private IEnumerator moveAndDestroy(int direction) {
		float xForce, yForce, zForce;
		if (direction == 0) { //Right
			xForce = Random.Range (SceneConstants.XFORCE_COLLISION_MIN, SceneConstants.XFORCE_COLLISION_MAX);
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX);
		} else if (direction == 1) { //Left
			xForce = Random.Range (-SceneConstants.XFORCE_COLLISION_MAX, -SceneConstants.XFORCE_COLLISION_MIN);
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX);
		} else {
			xForce = (Random.Range (-SceneConstants.XFORCE_COLLISION_MIN / 2, SceneConstants.XFORCE_COLLISION_MAX / 2)) * 2;
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX);
		}
			

		yForce = Random.Range (SceneConstants.YFORCE_COLLISION_MIN, SceneConstants.YFORCE_COLLISION_MAX);
		carRigidbody.AddForce (new Vector3 (xForce, yForce, zForce));
		carRigidbody.AddTorque (new Vector3 (xForce, yForce, zForce));

		//Time until destruction, change this
		for (int i = 0; i < SceneConstants.TIME_UNTIL_DESTROY_CAR; i++) {
			yield return null;
		}

		Destroy (gameObject);
	}

}
