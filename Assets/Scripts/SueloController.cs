using UnityEngine;
using System.Collections;

public class SueloController : MonoBehaviour {
    //private Rigidbody rb;
    public GameObject parentLaberinto;
    public float turnSpeed;
    // Use this for initialization
    void Start () {
       //rb = GetComponent<Rigidbody>();
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
        //rotateSuelo();
    }

    void rotateSuelo()
    {
        float rotationX = Input.GetAxis("Vertical");
        float rotationY = Input.GetAxis("Horizontal");

        //left and right
        if (Mathf.Abs(rotationX) > Mathf.Abs(rotationY))
        {
            if (rotationX > 0)
                transform.Rotate(Vector3.right, -turnSpeed * Time.deltaTime);
            else
                transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
        }

        //up and down
        if (Mathf.Abs(rotationX) < Mathf.Abs(rotationY))
        {
            if (rotationY < 0)
            {
                transform.Rotate(Vector3.forward, -turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
            }
        }
        float minRotation = -45;
        float maxRotation = 45;
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
