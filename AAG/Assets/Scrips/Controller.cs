using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float moveSpeed = 10;
	public float jumpHight = 10;

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
	Transform camTran;
	// Use this for initialization	
	void Start () {
		tran = gameObject.GetComponent <Transform> ();
		rigi = gameObject.GetComponent <Rigidbody> ();
		camTran = Camera.main.gameObject.GetComponent <Transform> ();
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
		cRotation = camTran.eulerAngles.y;
		angles = tran.eulerAngles;
		axis = Vector2.ClampMagnitude (new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 0.8f);
		xMovement = axis.x * Mathf.Cos (cRotation  * Mathf.Deg2Rad) + axis.y  * Mathf.Sin (cRotation * Mathf.Deg2Rad);
		zMovement = axis.y * Mathf.Cos (cRotation * Mathf.Deg2Rad) - axis.x * Mathf.Sin (cRotation  * Mathf.Deg2Rad); 

		if (axis.sqrMagnitude!= 0) {
			inputDirection = Mathf.Atan2 (xMovement, zMovement) * Mathf.Rad2Deg;
			yAngle = Mathf.SmoothDampAngle (angles.y, inputDirection, ref rence, (0.96f - axis.sqrMagnitude)/2);
			rigi.MoveRotation (Quaternion.AngleAxis (yAngle, Vector3.up));
		}

		rigi.AddForce (new Vector3(xMovement, 0, zMovement) * moveSpeed );
	}
}
