using UnityEngine;
using System.Collections;

public class AbilityController : MonoBehaviour {
	public GameObject sword;
	public float attackSpeed = 10;
	
	Vector3 weaponPostion;
	Vector3 weaponRotation;
	float attackTimeStamp;
	bool attack1 = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && !attack1) {
			attack1 = true;
			attackTimeStamp = Time.time;
		} 
		if (Time.time - attackTimeStamp > 1){
			attack1 = false;
			sword.GetComponent <Transform> ().position = weaponPostion;
			sword.GetComponent <Transform> ().eulerAngles = weaponRotation;
		}
	}
	
	void FixedUpdate () {
		if (attack1) {

		}
	}
}
