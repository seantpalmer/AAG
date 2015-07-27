using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float moveSpeed = 10;
	public float jumpHight = 10;
	public float hSensitivity = 16;
	public float vSensitivity = 9;

	float cRotation = 0;
	float xMovement;
	float zMovement;
	float inputDirection;
	float yAngle;
	float rence = 0.3f;
	Vector2 axis;
	Vector3 position;
	Vector3 velocity;
	Vector3 angles;
	Transform tran;
	Rigidbody rigi;
	CameraController camCon;
	// Use this for initialization	
	void Start () {
		tran = gameObject.GetComponent <Transform> ();
		rigi = gameObject.GetComponent <Rigidbody> ();
		camCon = gameObject.GetComponent <CameraController> ();
	}

	// Update is called once per frame	
	void Update () {
		position = tran.position;

		if (Input.GetButtonDown("Jump")) {
			velocity = rigi.velocity;
			rigi.velocity = new Vector3 (velocity.x, jumpHight, velocity.z);
		}
	}

	void FixedUpdate () {
		cRotation = camCon.hCameraRotation;
		angles = tran.eulerAngles;
		axis = Vector2.ClampMagnitude (new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 0.8f);
		xMovement = axis.x * Mathf.Cos (cRotation  * Mathf.Deg2Rad) + axis.y  * Mathf.Sin (cRotation * Mathf.Deg2Rad);
		zMovement = axis.y * Mathf.Cos (cRotation * Mathf.Deg2Rad) - axis.x * Mathf.Sin (cRotation  * Mathf.Deg2Rad); 

		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			inputDirection = Mathf.Atan2 (xMovement, zMovement) * Mathf.Rad2Deg;
			yAngle = Mathf.SmoothDampAngle (angles.y, inputDirection, ref rence, (0.96f - axis.sqrMagnitude)/2);
			rigi.MoveRotation (Quaternion.AngleAxis (yAngle, Vector3.up));
		}

		camCon.hCameraRotation += Input.GetAxisRaw ("Horizontal2") * hSensitivity;
		camCon.vCameraRotation += Input.GetAxisRaw ("Vertical2") * vSensitivity;
		rigi.AddForce (new Vector3(xMovement, 0, zMovement) * moveSpeed );
	}
}
