using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Transform playerTransform;
	public Camera topCamera;
	public Camera sideCamera;
	public Camera frontCamera;


	private bool inputEnabled = true;
	// Use this for initialization
	void Start () {


		playerTransform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame

	//Lol just change all this shit to lerps, maybe?
	void Update () {
		if (inputEnabled) {
			if (Input.GetKeyDown (KeyCode.A)) {
				//playerTransform.position = new Vector3 (playerTransform.position.x - 1, playerTransform.position.y, playerTransform.position.z);
				StartCoroutine(moveDirection(0));
			} else if (Input.GetKeyDown (KeyCode.D)) {
				//playerTransform.position = new Vector3 (playerTransform.position.x + 1, playerTransform.position.y, playerTransform.position.z);
				StartCoroutine(moveDirection(1));
			}
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			//playerTransform.position = new Vector3 (playerTransform.position.x - 1, playerTransform.position.y, playerTransform.position.z);
			//topCamera.transform 



		}

			
	}


	public IEnumerator moveDirection(int dir) {
		inputEnabled = false;
		if (dir == 0) { // Move Left/Up (negative X)
			Vector3 newPos = playerTransform.position;
			for (int i = 0; i < 30; i++) {
				newPos.x -= 0.1f;
				playerTransform.position = newPos;
				yield return null;
			}
		} else if (dir == 1) { // Move right/down (positive X)
			Vector3 newPos = playerTransform.position;
			for (int i = 0; i < 30; i++) {
				newPos.x += 0.1f;
				playerTransform.position = newPos;
				yield return null;
			}
		}
		inputEnabled = true;
	}

	//public IEnumerator moveCamera(int camSpot) {
		
	//}

}
