using UnityEngine;
using System.Collections;

public class BridgeSpawner : MonoBehaviour {

	public LevelSceneController levelSceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	/* To be implemented
	private int playerDifficulty = 1;
	private int playerLevel = 1;
	*/
	public GameObject bridgePrefab;

	void Awake() {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
	}

	void Start () {
		spawnInterval = SceneConstants.BASIC_BRIDGE_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_BRIDGE_SPAWN_TIME / 2;
		bridgePrefab.GetComponent<MiscObjectMover> ().levelSceneController = levelSceneController;
	}

	void Update () {
		if (timeUntilNextSpawn <= 0) {
			GameObject newBridge = Instantiate (bridgePrefab, new Vector3 (0, 0, SceneConstants.OBJECT_SPAWN_POSITION), Quaternion.identity) as GameObject;
			setupNewBridge (newBridge);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= (Time.deltaTime * levelSceneController.SCENE_SPEED);
	}

	//A new bridge object gets a MiscObjectMover component attached to it to move it through the scene
	private void setupNewBridge(GameObject bridge) {
		MiscObjectMover mover = bridge.GetComponent<MiscObjectMover> ();
		mover.setVelocity (SceneConstants.BASE_OBJECT_VELOCITY);
		float width = Random.Range (1.0f, 4f);
		bridge.transform.localScale = new Vector3 (1, 1, width);
	}
}
