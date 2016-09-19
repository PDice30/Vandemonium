using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public Light mainLightSource;


	public Light SunSource;
	public Light NightSource;
	public Light PlayerSpotlight;


	private float lightChangeInterval = 3.0f;
	private float timeUntilLightChange = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//mainLightSource.transform.Rotate (Vector3.right);
		/*
		if (timeUntilLightChange <= 0) {
			mainLightSource.transform.Rotate (Vector3.right);
			timeUntilLightChange = lightChangeInterval;
			Debug.Log (mainLightSource.transform.rotation.eulerAngles.x);

		}

		if (mainLightSource.transform.rotation.eulerAngles.x < 360
		    && mainLightSource.transform.rotation.eulerAngles.x > 250) {
			PlayerSpotlight.GetComponent<Light> ().enabled = true;
		} else {
			PlayerSpotlight.GetComponent<Light> ().enabled = false;
		}
	
		if (Input.GetKeyDown (KeyCode.N)) {
			SunSource.GetComponent<Light> ().enabled = !SunSource.GetComponent<Light> ().enabled;
			NightSource.GetComponent<Light> ().enabled = !NightSource.GetComponent<Light> ().enabled;
			PlayerSpotlight.GetComponent<Light> ().enabled = !PlayerSpotlight.GetComponent<Light> ().enabled;
		}
*/
		timeUntilLightChange -= (Time.deltaTime);



	}
}
