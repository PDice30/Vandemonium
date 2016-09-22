using UnityEngine;
using System.Collections;

public class CubeRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0.2f, 0.7f, 0.2f));
	}
}
