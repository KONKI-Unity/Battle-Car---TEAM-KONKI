using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runcar : MonoBehaviour {

	public Text TxtSpeed;
	public WheelCollider front_left;
	public WheelCollider front_right;
	public WheelCollider back_left;
	public WheelCollider back_right;
	public float Torque;
	public float Speed;
	public float MaxSpeed = 200f;
	public int Brake = 10000;
	public float CoefAccel = 10f;
	public float WheelAngleMax = 10f;
	public float DAmax = 40f;
	public bool freinage = false ;


	void Update () {
		//affichage de la vitesse
		/*Speed = GetComponent<Rigidbody>().velocity.magnitude *3.6f;
		TxtSpeed.text = "Speed : " + (int)Speed;

*/
		// acceleration
		if (Input.GetKey (KeyCode.UpArrow)&& Speed < MaxSpeed) {
			if (!freinage) {
				front_left.brakeTorque = 0;
				front_right.brakeTorque = 0;
				back_left.brakeTorque = 0;
				back_left.brakeTorque = 0;
				back_left.motorTorque = Input.GetAxis ("Vertical") * Torque * CoefAccel * Time.deltaTime;
				back_right.motorTorque = Input.GetAxis ("Vertical") * Torque * CoefAccel * Time.deltaTime;
			}

		}	

		//décélération
		/*if (!Input.GetKey (KeyCode.UpArrow) && !freinage || Speed > MaxSpeed ) {

			back_left.motorTorque = 0;
			back_right.motorTorque = 0;
			back_left.brakeTorque = Brake * CoefAccel * Time.deltaTime;
			back_right.brakeTorque = Brake * CoefAccel * Time.deltaTime;
		}
*/
		//dirrection
		float DA = (WheelAngleMax - DAmax)/MaxSpeed+DAmax;
		Debug.Log (DA);
		front_left.steerAngle = Input.GetAxis("Horizontal") * DA;
		front_right.steerAngle = Input.GetAxis("Horizontal") * DA;

		//freinage
		if (Input.GetKey (KeyCode.DownArrow))
		{
			freinage = true;
			back_left.brakeTorque = Mathf.Infinity;
			back_right.brakeTorque = Mathf.Infinity;
			front_left.brakeTorque = Mathf.Infinity;
			front_right.brakeTorque = Mathf.Infinity;
			back_left.brakeTorque = 0;
			back_right.brakeTorque = 0;
		} 
		else 
		{
			freinage = false;
		}

	}

	void start() 
	{
		//centre de masse
		GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f);
	}
}
