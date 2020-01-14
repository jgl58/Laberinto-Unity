using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartidaSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    public int contadorNiveles = 1;
    public static PartidaSingleton instance;
    public static PartidaSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PartidaSingleton>();

                if (instance == null)
                {
                    GameObject container = new GameObject("Singleton");
                    instance = container.AddComponent<PartidaSingleton>();
                }
            }

            return instance;
        }
    }
    void Start()
    {
        
    }

    public void aumentarNivel()
    {
        contadorNiveles++;
    }
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
