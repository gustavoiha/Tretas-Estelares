using UnityEngine;
using System.Collections;

/**
*
* Observation: currently, this file animates the Sith character and lets the gamer play with it (walk, turn and draw Saber)
* In the future, this script will be called PlayerBehaviour and will animate the Jedi
* A whole new script will be made for the Sith, which will move by itself
*
**/

public class SithBehaviour : MonoBehaviour {

	public Transform Saber;

	private Animator animator;
	private Rigidbody rigidBody;
	private Transform camera;

	// Height of the center of the camera's rotation (for example, the player body's center
	public float CameraPivot = 2.5f;

	public float CameraAngle = 35.0f;
	public float CameraLookAngle = 10.0f;

	// Camera's distance from Pivot
	public float CameraDistance = 9.5f;

	public float moveSpeedFoward = 6.0f;
	public float moveSpeedSides  = 6.0f;
	public float turnSpeedX = 60.0f;
	public float turnSpeedY = 60.0f;

	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	// Use this for initialization
	void Start () {
		animator  = gameObject.GetComponentInChildren<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		camera    = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("b") && !animator.GetBool("hasSaber")) {
			Instantiate (Saber, Vector3.zero, Quaternion.identity);
			animator.SetInteger ("AnimParam", 2);
		}

		/**
		 * Movement animations
		 */
		if (Input.GetKeyUp("w")){
			animator.SetInteger ("AnimParam", 0);
		}

		if (Input.GetKeyDown ("w")/* && !isDrawingSaber*/) {
			animator.SetInteger ("AnimParam", 1);
		}

		doTranslation ();
		doRotation ();
	}

	private void doTranslation(){
		if (shouldMove()) {
			//if (controler.isGrounded) {
				moveDirection = Vector3.zero;
				moveDirection += transform.forward * Input.GetAxis ("VerticalTranslation")   * moveSpeedFoward;
				moveDirection += transform.right   * Input.GetAxis ("HorizontalTranslation") * moveSpeedSides;
				
				transform.position += moveDirection * Time.deltaTime;
			//}
		}
	}

	private void doRotation(){

		// Stoping player from rotating automatially because of the rigdbody component
		rigidBody.angularVelocity = new Vector3(0,0,0);

		// Making sure the player is always perpendicular
		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (0, transform.rotation.eulerAngles.y, 0);

		float turnX = Input.GetAxis ("Mouse X") + Input.GetAxis ("HorizontalRotation");
		float turnY = Input.GetAxis ("Mouse Y") + Input.GetAxis ("VerticalRotation");

		transform.rotation = quaternion;
		transform.Rotate (0, turnX * turnSpeedX * Time.deltaTime, 0);

		//float camPosX = - CameraDistance * Mathf.Cos(camera.rotation.eulerAngles.y * Mathf.Deg2Rad) * Mathf.Cos(camera.rotation.eulerAngles.x * Mathf.Deg2Rad);
		float camPosY = CameraPivot + CameraDistance * Mathf.Sin(camera.rotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos(camera.rotation.eulerAngles.z * Mathf.Deg2Rad);
		float camPosZ = - CameraDistance * Mathf.Cos(camera.rotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos(camera.rotation.eulerAngles.z * Mathf.Deg2Rad);;

		camera.Rotate (turnY * turnSpeedY * Time.deltaTime, 0, 0);
		camera.localPosition = new Vector3 (0, camPosY, camPosZ);
	}

	// returns true if player is in walking or running animation
	private bool shouldMove(){
		return animator.GetCurrentAnimatorStateInfo (0).IsName ("walking") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("running") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("walkingSaber") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("runningSaber");
	}
}
