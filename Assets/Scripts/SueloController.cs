using UnityEngine;
using System.Collections;

public class SueloController : MonoBehaviour {
    private Rigidbody rb;
    public GameObject parentLaberinto;
    public float turnSpeed;
    // Use this for initialization
    void Start () {
       rb = GetComponent<Rigidbody>();
	    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
	}

    private void Awake()
    {
        parentLaberinto = GameObject.FindGameObjectWithTag("Laberinto");
    }

    // Update is called once per frame
    void Update () {
        float zangle = Input.GetAxis("Horizontal");
        float xangle = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(xangle, 0, zangle));
	}

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
    }
}
