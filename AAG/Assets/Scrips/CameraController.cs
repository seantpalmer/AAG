using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float vCameraRotation = 45;
	public float hCameraRotation = 0;
	public float restingDistance = 30;
	public float restingAngle = 45;
	public float upperCameraLimit = 90;
	public float lowerCameraLimit = -90;
	public float hSensitivity = 32;
	public float vSensitivity = 18;
	public LayerMask layerMask = 1 << 8;

	Vector2 axis1;
	Vector2 axis2;
	float playerPosX;
	float playerPosY;
	float playerPosZ;
	float camPosX;
	float camPosY;
	float camPosZ;
	float camDist;
	float rence;
	float renceH;
	float renceV;
	Transform tran;
	Transform camTran;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		tran = gameObject.GetComponent <Transform> ();
		camTran = Camera.main.GetComponent <Transform> ();
		camDist = restingDistance;
	}
	
	// Update is called once per frame
	void Update () {
		playerPosX = tran.position.x;
		playerPosY = tran.position.y;
		playerPosZ = tran.position.z;
		axis1 = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		axis2 = new Vector2 (Input.GetAxisRaw ("Horizontal2"), Input.GetAxisRaw ("Vertical2"));

		if (hCameraRotation > 360) {
			hCameraRotation -= 360;
		}
		if (hCameraRotation < 0) {
			hCameraRotation += 360;
		}
			
		if (Physics.Raycast (tran.position, camTran.position - tran.position, out hit, restingDistance, layerMask)) {
			camDist = Mathf.SmoothDamp(camDist, hit.distance, ref rence, 0.1f);
		} else {
			camDist = Mathf.SmoothDamp(camDist, restingDistance, ref rence, 0.6f);
		}


		vCameraRotation += axis2.y * vSensitivity * Time.deltaTime;

		if (axis2.sqrMagnitude == 0) {
			hCameraRotation = Mathf.SmoothDampAngle (hCameraRotation, tran.eulerAngles.y, ref renceH, 0.55f);
			vCameraRotation = Mathf.SmoothDampAngle (vCameraRotation, restingAngle, ref renceV, 0.2f);
		} else {
			hCameraRotation += axis2.x * hSensitivity * Time.deltaTime;
			vCameraRotation += axis2.y * vSensitivity * Time.deltaTime;
		}

		if (vCameraRotation > upperCameraLimit) {
			vCameraRotation = upperCameraLimit;
		} 
		if (vCameraRotation < lowerCameraLimit) {
			vCameraRotation = lowerCameraLimit;
		} 

		camPosX = playerPosX + camDist * Mathf.Sin (-hCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad);
		camPosY = playerPosY + camDist * Mathf.Sin (vCameraRotation * Mathf.Deg2Rad);
		camPosZ = playerPosZ - camDist * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (-hCameraRotation * Mathf.Deg2Rad);

		camTran.eulerAngles = new Vector3 (vCameraRotation, hCameraRotation, 0);

		camTran.position = new Vector3 (camPosX, camPosY, camPosZ);

	}
	// FixedUpdate is called fifty times per second
	void FixedUpdate () {

	}

}
