using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

    public Text countText;
    public Text winText;
	public Text nivel;
	public GameObject vida1, vida2, vida3;
	

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
		if(rb.position.y < -10.5f){
			
			instance.vidas--;
			if(instance.vidas > 0)
			{
				resetNivel();
				gestionVidas();
			}
			else
			{
				winText.text = "¡¡Perdiste!!";
				gestionVidas();
				Invoke("QuitGame", 1.5f);
			}

		}

       	
	}

	void gestionVidas()
	{
		switch (instance.vidas)
		{
			case 3:
				vida1.SetActive(true);
				vida2.SetActive(true);
				vida3.SetActive(true);
				break;
			case 2:
				vida1.SetActive(true);
				vida2.SetActive(true);
				vida3.SetActive(false);
				break;
			case 1:
				vida1.SetActive(true);
				vida2.SetActive(false);
				vida3.SetActive(false);
				break;
			default:
				vida1.SetActive(false);
				vida2.SetActive(false);
				vida3.SetActive(false);
				break;
		}
	}

	void resetNivel()
	{
		GameObject pelota = GameObject.FindGameObjectWithTag("Player");
		pelota.transform.position = new Vector3(-4f, 1f, -4f);

		GameObject laberinto = GameObject.FindGameObjectWithTag("Laberinto");
		laberinto.transform.eulerAngles = new Vector3(0, 0, 0);
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("radar")) {
			other.gameObject.SetActive (false);
			contador = contador + 1;
			SetCountText ();

            if (GameObject.FindGameObjectsWithTag("radar").Length == 0)
            {
         
                winText.text = "¡¡Ganaste!!";
                instance.contadorNiveles++;
				instance.vidas = 3;
				
				if(instance.nMax < 17 && instance.mMax < 17)
				{
					instance.nMax += 2;
					instance.mMax += 2;
				}
				gestionVidas();
                Invoke("CargarNivel",1.5f);
            }
        }

    }

    void CargarNivel()
    {
		SceneManager.LoadScene(0);
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