using UnityEngine;
using System.Collections;

public class LightSaberBehaviour : MonoBehaviour {

	private Transform someBone;

	// Use this for initialization
	void Start () {

		/**
		 * Use this to find the correct bone to attach the lightsaber.
		 * Don't forget to give the bone the tag you wish!
		 * It won't work otherwise.
		 */
		someBone = GameObject.FindWithTag("SithHandR").transform;
		placeInHand ();

		Debug.Log ("init saber");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Use this method to place the light saber on the hand
	public void placeInHand(){
		Transform someTransform = gameObject.transform;
		someTransform.parent = someBone;
		someTransform.localPosition = Vector3.zero;
		someTransform.localRotation = Quaternion.identity * Quaternion.Euler(90, 0, 0);
	}
}
