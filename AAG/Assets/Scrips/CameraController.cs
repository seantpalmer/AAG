using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float maxDist = 20;
	public float minDist = 10;
	public float restAngle = 30;

	Transform tran;
	Transform camTran;
	Vector3 tracker;
	// Use this for initialization
	void Start () {
		tran = GetComponent <Transform> ();
		camTran = Camera.main.GetComponent <Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		//camTran.LookAt (tran);
		camTran.position = new Vector3 (tran.position.x, tran.position.y + minDist, tran.position.z - maxDist);
	}

	// FixedUpdate is called fifty times per second
	void FixedUpdate () {

	}

}
