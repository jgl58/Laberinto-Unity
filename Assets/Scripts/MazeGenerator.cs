using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject parentLaberinto;
    public GameObject pared;
    public GameObject suelo;
    public GameObject hueco;
    public GameObject premio;

    public PartidaSingleton instance;

    public static int nMax;//impar
    public static int mMax;//impar
    public static int[,] M;
    const int BLOQUE = 0;
    const int LIBRE = 1;

    void Awake()
    {
        instance = PartidaSingleton.Instance;
    }

    void agregarParOrdenado(int[,] A, ref int k, int a, int b)
{
    bool Esta = false;
    for (int i = 0; i < k; i++)
    {
        if (a == A[i,0] && b == A[i,1])
        {
            Esta = true;
            break;
        }
    }
    if (Esta == false)
    {
        A[k,0] = a;
        A[k,1] = b;
        k++;
    }
}

    void obtenerParOrdenadoRandom(int[,]a, ref int k,ref int x,ref int y)
{
        int index = Random.Range(0,k); //range 0 to k-1

        x = a[index,0];
    y = a[index,1];
    for (int i = index; i < k-1; i++)
    {
        a[i,0] = a[i + 1,0];
        a[i,1] = a[i + 1,1];
    }
    k--;
}


    void crearLaberinto(int[,] M, int m, int n)
{
        int[,] ID_VECINOS_BLOQUEADOS = new int[m + n, 2];
        int [,] ID_VECINOS_DESBLOQUEADOS = new int[4,2];
    int kb = 0, kd = 0, maxBlock = 0;
    for(int i=0;i<m;i++){
        for(int j=0;j<n;j++){
            M[i,j]=BLOQUE;
        }
    }
    int Oi, Oj, Ii, Ij;
    Oi=1; Oj=1;Ii = 0;Ij = 0;
    M[Oi,Oj]=LIBRE;
    if(Oj-2>0   && M[Oi,Oj - 2]==BLOQUE){
        agregarParOrdenado(ID_VECINOS_BLOQUEADOS,ref kb, Oi, Oj-2);
    }
    if(Oj+2<n && M[Oi,Oj + 2]==BLOQUE){
        agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi, Oj+2);
    }
    if(Oi-2>0 && M[Oi - 2,Oj]==BLOQUE){
        agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi-2, Oj);
    }
    if(Oi +2< m && M[Oi + 2,Oj]==BLOQUE){
        agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi+2, Oj);
    }
    int count = 0;
    while(kb>0){
        if(maxBlock<kb){
            maxBlock=kb;
        }
        obtenerParOrdenadoRandom(ID_VECINOS_BLOQUEADOS, ref kb, ref Oi, ref Oj);//a) y g)
                                                                    //agregando vecinos desbloqueados
        kd=0;
         if(Oj-2>0   && M[Oi,Oj - 2]==LIBRE){
            agregarParOrdenado(ID_VECINOS_DESBLOQUEADOS, ref kd, Oi, Oj-2);
        }
        if(Oj+2<n && M[Oi,Oj + 2]==LIBRE){
            agregarParOrdenado(ID_VECINOS_DESBLOQUEADOS, ref kd, Oi, Oj+2);
        }
        if(Oi-2>0 && M[Oi - 2,Oj]==LIBRE){
            agregarParOrdenado(ID_VECINOS_DESBLOQUEADOS, ref kd, Oi-2, Oj);
        }
        if(Oi +2< m && M[Oi + 2,Oj]==LIBRE){
            agregarParOrdenado(ID_VECINOS_DESBLOQUEADOS, ref kd, Oi+2, Oj);
        }
        obtenerParOrdenadoRandom(ID_VECINOS_DESBLOQUEADOS, ref kd, ref Ii, ref Ij);//c)
        if(Ii == Oi){//d)
            if(Ij<Oj){
                 M[Ii,Ij + 1]=LIBRE;
            }else{
                 M[Ii,Oj + 1]=LIBRE;
            }
        }else{
            if(Ii<Oi){
                  M[Ii + 1,Ij]=LIBRE;
            }else{
                 M[Oi + 1,Ij]=LIBRE;
            }
        }
         M[Oi,Oj]=LIBRE; //f)
         //agregando vecinos bloqueados
        if(Oj-2>0   && M[Oi,Oj - 2]==BLOQUE){
            agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi, Oj-2);
        }
        if(Oj+2<n && M[Oi,Oj + 2]==BLOQUE){
            agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi, Oj+2);
        }
        if(Oi-2>0 && M[Oi - 2,Oj]==BLOQUE){
            agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi-2, Oj);
        }
        if(Oi +2< m && M[Oi + 2,Oj]==BLOQUE){
            agregarParOrdenado(ID_VECINOS_BLOQUEADOS, ref kb, Oi+2, Oj);
        }
        count++;
    }
   
}
 
   

    private void Start()
    {

        nMax = PartidaSingleton.instance.nMax;
        mMax = PartidaSingleton.instance.mMax;
        M = new int[mMax, nMax];
        crearLaberinto(M, mMax, nMax);
        parentLaberinto = GameObject.FindGameObjectWithTag("Laberinto");
        for (int i = 0; i < mMax; i++)
        {
            for (int j = 0; j < nMax; j++)
            {
                if (M[i, j] == BLOQUE)
                {
                    pared.transform.localScale = new Vector3(1f, 1f, 1f);

                    GameObject auxPared = Instantiate(pared, new Vector3(i-(mMax/2), 0, j-(nMax/2)), new Quaternion());
                    auxPared.transform.parent = parentLaberinto.transform;
                }
                if (M[i, j] == LIBRE)
                {
                    int random = Random.Range(0, 11);
                    //Si estamos en la salida no queremos hueco
                    if (i == 1 && j == 1)
                    {
                        random = 2;
                    }
                    if (random == 1)
                    {
                        //Instanciamos hueco
                        hueco.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                        //Agregamos una rotacion aleatoria para el hueco del suelo
                        int giro = Random.Range(0, 1);
                        int rotar = 90;
                        if (giro == 0)
                        {
                            rotar = -90;
                        }
                        GameObject auxHueco = Instantiate(hueco, new Vector3(i - (mMax / 2), -1, j - (nMax / 2)), Quaternion.Euler(90, rotar, 0));
                        auxHueco.transform.parent = parentLaberinto.transform;
                    }
                    else
                    {
                        //Instanciamos suelo liso
                        suelo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        
                        GameObject auxSuelo = Instantiate(suelo, new Vector3(i - (mMax / 2), -1, j - (nMax / 2)), Quaternion.Euler(90, 0, 0));
                        auxSuelo.transform.parent = parentLaberinto.transform;
                        int pongoPremio = Random.Range(0, 11);
                        if (pongoPremio == 1)
                        {
                            premio.transform.localScale = new Vector3(0.5f, 0.2f, 0.5f);
                            GameObject auxPremio = Instantiate(premio, new Vector3(i - (mMax / 2), 0, j - (nMax / 2)), Quaternion.Euler(-90, 0, 0));
                            auxPremio.transform.parent = parentLaberinto.transform;
                        }
                    }
                }
            }
        }
        
    }
}
