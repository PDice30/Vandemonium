using UnityEngine;
using System.Collections;

public class TerrainSpawner : MonoBehaviour {

	public SceneController sceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	public GameObject[] treePrefabs = new GameObject[5]; 
	// Use this for initialization
	void Start () {
		spawnInterval = SceneConstants.BASIC_TERRAIN_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_TERRAIN_SPAWN_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			GameObject newTerrain = Instantiate (treePrefabs[chooseTerrain(treePrefabs.Length)], new Vector3 (getXPos(), 0, 25), Quaternion.identity) as GameObject;
			setupNewTerrain (newTerrain);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= (Time.deltaTime * sceneController.SCENE_SPEED);
	}

	//Set up the position of the new car, its speed, etc.
	private void setupNewTerrain(GameObject terrain) {
		MiscObjectMover mover = terrain.AddComponent<MiscObjectMover> ();
		mover.sceneController = sceneController;
		mover.setVelocity (SceneConstants.BASE_OBJECT_VELOCITY);
	}

	private int chooseTerrain(int numberOfObjects) {
		return Random.Range (0, numberOfObjects);
	}

	private float getXPos() {
		int side = Random.Range (0, 2);

		if (side == 0) { //Left Side - x between -8 and -14
			return Random.Range(-14f, -8f);
		} else { //Right Side - x between 8 and 14
			return Random.Range(8f, 14f);
		}
	}
}
