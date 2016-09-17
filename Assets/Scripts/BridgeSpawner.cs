using UnityEngine;
using System.Collections;

public class BridgeSpawner : MonoBehaviour {

	public PlayerController playerController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	private int playerDifficulty = 1;
	private int playerLevel = 1;
	private int carLevel = 1;

	public GameObject bridgePrefab;

	void Start () {
		spawnInterval = SceneConstants.BASIC_BRIDGE_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_BRIDGE_SPAWN_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			GameObject newBridge = Instantiate (bridgePrefab, new Vector3 (0, 0, 25), Quaternion.identity) as GameObject;
			randomizeBridgeStats (newBridge);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= Time.deltaTime;
	}

	private void randomizeBridgeStats(GameObject bridge) {
		float width = Random.Range (0.2f, 1f);
		bridge.GetComponent<MiscObjectMover> ().setVelocity (SceneConstants.BASE_BRIDGE_VELOCITY);
		bridge.transform.localScale = new Vector3 (1, 1, width);
	}
}
