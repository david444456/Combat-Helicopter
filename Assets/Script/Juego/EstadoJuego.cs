using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;
    
    public float valor = 1f;
    string nombreArchivo;
    public int marcador;
    public bool vivoHelicONo = true;
    public string nombreSceneActual;
    public int nombreSceneSig;
    public bool IfIWin = false;

    [Header("Datos Para reinicar")]
    public int vidasComienzoBase;
    public int vidasComienzoHelic;

    [Header("NivelVariables")]
   public int vidasNivel;
    public bool RENivel;
    public int vidasBase;
    public int numeroHelic = 1;
    public bool helicRojo;
    public bool helicVerde;
    public int Helicopter;
    public int AnunciosPorNivel;
    public bool AnunciosBoolNivel;

    public int DateAdsValueX2;
    [Header("Moneda")]
    public int valorVida;
    public int valorRE;
    public int valorVidaBase;
    public int ValorMoneda;
    GameObject monedas;
    GameObject textMonedas;

    public bool audioPrendido = true;
    private void Awake()
    {
        buscarObjetosPorScene();
         nombreArchivo = Application.persistentDataPath + "/datos.dat";
        if (estadoJuego == null)
        {
            estadoJuego = this;
        }
        else if (estadoJuego != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

   public  void resetVidasyRePorNivel() {
        
        vidasNivel = 0;
        RENivel = false;
        vidasBase = 0;
    }
    // Use this for initialization
    void Start()
    {
       // volumen = GetComponent<Volumen>();
        Cargar();
        textMonedas.GetComponent<Text>().text = ValorMoneda.ToString();
        
    }
    private void Update()
    {
    }
    public void buscarObjetosPorScene() {
     //   PlayerPrefs.SetInt("Nivel", 0);

        nombreSceneActual = SceneManager.GetActiveScene().name;
        if (!(SceneManager.GetActiveScene().name == "Menu"))
        { 
        var plHeal = FindObjectOfType<PlayerHealth>();
        nombreSceneSig = plHeal.EsceneSig;
        }
        monedas = FindObjectOfType<Moneda>().gameObject;
        textMonedas = monedas.transform.Find("TextMoneda").gameObject;
        textMonedas.GetComponent<Text>().text = ValorMoneda.ToString();

    }

    public void incrementarValor(int intmonedas) {
        ValorMoneda += intmonedas;
        textMonedas.GetComponent<Text>().text = ValorMoneda.ToString();
        Guardar();
    }

    public void incrementarVida( int intVida) {
        valorVida = intVida;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(nombreArchivo);

        DatoAGuardar datos = new DatoAGuardar();
        
        datos.RE = valorRE;
        datos.vidasBase = valorVidaBase;
        datos.valor = ValorMoneda;
        datos.vidas += intVida;

        datos.helicVerde = helicVerde;
        datos.helicRojo = helicRojo;

        bf.Serialize(file, datos);
        file.Close();
    }
    public void incrementarRE( int REint) {
        valorRE = REint;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(nombreArchivo);

        DatoAGuardar datos = new DatoAGuardar();
        datos.vidas = valorVida;
        
        datos.vidasBase = valorVidaBase;
        datos.valor = ValorMoneda;
        datos.RE += REint;
        datos.helicVerde = helicVerde;
        datos.helicRojo = helicRojo;
        bf.Serialize(file, datos);
        file.Close();
    }
    public void incrementarVidasBase(int VidasBaseInt)
    {
        valorVidaBase = VidasBaseInt;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(nombreArchivo);

        DatoAGuardar datos = new DatoAGuardar();
        datos.vidas = valorVida;
        datos.RE = valorRE;

        datos.valor = ValorMoneda;
        datos.vidasBase += VidasBaseInt;
        datos.helicVerde = helicVerde;
        datos.helicRojo = helicRojo;
        bf.Serialize(file, datos);
        file.Close();
    }
    public void Guardar()
     {
         BinaryFormatter bf = new BinaryFormatter();
         FileStream file = File.Create(nombreArchivo);
         DatoAGuardar datos = new DatoAGuardar();
         datos.vidas = valorVida;
         datos.RE = valorRE;
         datos.vidasBase = valorVidaBase;
        datos.helicVerde = helicVerde;
        datos.helicRojo = helicRojo;
        datos.HelicV = Helicopter;

        print(datos.helicRojo + " Rojo");
        print(datos.helicVerde + " verde");

        datos.valor += ValorMoneda;
         bf.Serialize(file, datos);
         file.Close();
     }
    void Cargar()
    {
        if (File.Exists(nombreArchivo))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(nombreArchivo, FileMode.Open);

            DatoAGuardar datos = (DatoAGuardar)bf.Deserialize(file);

            ValorMoneda = datos.valor;
            valorVida = datos.vidas;
            valorRE = datos.RE;
            valorVidaBase = datos.vidasBase;
            helicVerde = datos.helicVerde;

            switch (datos.HelicV) {
                case 0:
                    break;
                case 1:
                    helicVerde = true;
                    break;
                case 2:
                    helicRojo = true;
                    break;
                case 3:
                    helicRojo = true;
                    helicVerde = true;
                    break;
            }

            helicRojo = datos.helicRojo;
            print(datos.HelicV);
            print(datos.helicRojo + " Rojo " + helicRojo);
            print(datos.helicVerde + " verde "+helicVerde);
            //helicComp = datos.HelicBool;
            file.Close();
        }
        else
        {
            print("no cargo");
            ValorMoneda = 0;
            valorVida = 0;
            valorRE = 0;
            valorVidaBase = 0;
        }
    }
    public void GuardarNivel(int nivelCompletado)
    {
        if (PlayerPrefs.GetInt("Nivel") < nivelCompletado)
        {
            PlayerPrefs.SetInt("Nivel", nivelCompletado);
            Debug.Log(nivelCompletado);
        }

    }
    public void CargarNivelNumero()
    {
        if (marcador < PlayerPrefs.GetInt("Nivel"))
        {
            marcador = PlayerPrefs.GetInt("Nivel");
            Debug.Log(marcador);

        }

    }

    public int GetScoreLevel() {
        return marcador;
    }
}
[Serializable]
class DatoAGuardar
{
    public int HelicV;

    public bool helicRojo;
    public bool helicVerde;
    public int vidas;
    public int RE;
    public int vidasBase;
    public int valor;
}



