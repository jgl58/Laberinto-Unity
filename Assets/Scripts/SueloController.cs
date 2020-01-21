using UnityEngine;
using System.Collections;

public class SueloController : MonoBehaviour {
    //private Rigidbody rb;
    public GameObject parentLaberinto;
    // Use this for initialization
    void Start () {
       //rb = GetComponent<Rigidbody>();
       }

    private void Awake()
    {
        parentLaberinto = GameObject.FindGameObjectWithTag("Laberinto");
    }

    // Update is called once per frame
    void FixedUpdate () {
        float posH = Input.GetAxis("Horizontal");
        float posV = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(posV, 0, posH));
    }
}
