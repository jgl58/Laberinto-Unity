using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private float posH, posV;
	public float velocidad;

	

 
	void Update()
	{
		float posH = Input.GetAxis("Horizontal");
		float posV = Input.GetAxis("Vertical");

		rotate();
	}
	// Update is called once per frame
	void rotate()
	{
		transform.Rotate(new Vector3(posV *velocidad, posH * velocidad, 0f));
	}
}
