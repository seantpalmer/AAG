using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float vCameraRotation = 45;
	public float hCameraRotation = 0;
	public float restingDistance = 30;
	public float upperCameraLimit = 90;
	public float lowerCameraLimit = -90;
	public LayerMask layerMask = 1 << 8;

	Vector3 playerPos;
	float camPosX;
	float camPosY;
	float camPosZ;
	float camDist;
	float rence;
	Rigidbody rigi;
	Transform camTran;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		rigi = gameObject.GetComponent <Rigidbody> ();
		camTran = Camera.main.GetComponent <Transform> ();
		camDist = restingDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if (hCameraRotation > 360) {
			hCameraRotation -= 360;
		}
		if (hCameraRotation < 0) {
			hCameraRotation += 360;
		}
		if (vCameraRotation > upperCameraLimit) {
			vCameraRotation = upperCameraLimit;
		} 
		if (vCameraRotation < lowerCameraLimit) {
			vCameraRotation = lowerCameraLimit;
		} 
			
		if (Physics.Raycast (rigi.position, camTran.position - rigi.position, out hit, restingDistance, layerMask)) {
			camDist = Mathf.SmoothDamp(camDist, hit.distance, ref rence, 0.1f);
		} else {
			camDist = Mathf.SmoothDamp(camDist, restingDistance, ref rence, 0.6f);
		}

		playerPos = rigi.position;
		//camPosX = playerPos.x + restingDistance * Mathf.Sin (-hCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad);
		//camPosY = playerPos.y + restingDistance * Mathf.Sin (vCameraRotation * Mathf.Deg2Rad);
		//camPosZ = playerPos.z - restingDistance * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (-hCameraRotation * Mathf.Deg2Rad);

		camPosX = playerPos.x + camDist * Mathf.Sin (-hCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad);
		camPosY = playerPos.y + camDist * Mathf.Sin (vCameraRotation * Mathf.Deg2Rad);
		camPosZ = playerPos.z - camDist * Mathf.Cos (vCameraRotation * Mathf.Deg2Rad) * Mathf.Cos (-hCameraRotation * Mathf.Deg2Rad);

		camTran.position = new Vector3 (camPosX, camPosY, camPosZ);
		camTran.eulerAngles = new Vector3 (vCameraRotation, hCameraRotation, 0);
		Debug.DrawRay (rigi.position, camTran.position - rigi.position);
	}

	void FixedUpdate () {

	}

}
