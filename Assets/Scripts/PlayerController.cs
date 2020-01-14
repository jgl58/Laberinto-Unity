using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour {

	public float velocidad;
    public Text countText;
    public Text winText;
	public Text nivel;
	private int contadorNivel = 1;
	

	private Rigidbody rb;
    private int contador ;

	public PartidaSingleton instance;
	void Awake()
	{
		instance = PartidaSingleton.Instance;
	}

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
        contador = 0;
        SetCountText();
        winText.text = "";
		nivel.text = "Nivel " + instance.contadorNiveles;

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        float posH = Input.GetAxis("Horizontal");
        float posV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(posH, 0.0f, posV);
		if(rb.position.y < -2.5f){
			winText.text = "Perdiste!!";
			instance.contadorNiveles++;
			
			Invoke ("QuitGame", 1.5f);	
		}else{
			rb.AddForce(movimiento * velocidad);
		}

       	
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag ("mono")) {
			other.gameObject.SetActive (false);
			contador = contador + 1;
			SetCountText ();

            if (GameObject.FindGameObjectsWithTag("mono").Length == 0)
            {
                Debug.Log(GameObject.FindGameObjectsWithTag("mono").Length);
                winText.text = "Ganaste!!";
                instance.contadorNiveles++;
                Invoke("CargarNivel",1.5f);
            }
        }

    }

    void CargarNivel()
    {
        Application.LoadLevel(0);
    }

	void SetCountText()
    {
            countText.text = "Contador: " + contador.ToString();

    }

	void QuitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		Application.OpenURL(webplayerQuitURL);
		#else
		Application.Quit();
		#endif
	}

}