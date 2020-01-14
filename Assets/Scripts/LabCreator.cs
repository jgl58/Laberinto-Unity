using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabCreator : MonoBehaviour
{

    public GameObject pared;
    public GameObject lab;

    public int x, y, z, repeticiones;

    public float offset;

    public List<GameObject> paredes;

    void Start()
    {
        for (int i = 0; i < repeticiones; i++)
        {
            paredes.Add(Instantiate(pared, new Vector3(x + (offset * i), y, z), new Quaternion(), lab.transform));
        }
    }

    private void Awake()
    {

        paredes = new List<GameObject>();
    }
}
