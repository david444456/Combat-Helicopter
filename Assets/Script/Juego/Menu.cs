using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    [Header("Joybutton")]
    [SerializeField] JoyButton inicioJoy;
    [SerializeField] JoyButton opcionesJoy;
    [SerializeField] JoyButton salirJoy;
    [SerializeField] JoyButton salirBoton;
    [SerializeField] JoyButton creditos;

    [Header("GO de escena")]
    [SerializeField] GameObject ui;
    [SerializeField] GameObject opcionesGO;
    [SerializeField] GameObject MenuInicioGO;
    [SerializeField] GameObject credits;
    [Header("Tienda")]
    [SerializeField] JoyButton helic;
    [SerializeField] JoyButton vidas;
    [SerializeField] GameObject memuUI;
    [SerializeField] GameObject tiendaComprar;
    [SerializeField] GameObject helicTienda;
    [SerializeField] GameObject vidasTienda;
    [SerializeField] GameObject fondo;

    [Header("TiendaVida")]
    [SerializeField] JoyButton JoyTieVida;
    [SerializeField] GameObject tiendaVida;

    [Header("Niveles")]
    [SerializeField] GameObject itemsMenu; 
    [SerializeField] JoyButton [] nivel1;

    [Header("Funcionamiento script")]
    RaycastHit2D hit;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    [SerializeField]LayerMask layer;

    Camera camera;

    
    [Header("Audio")]
    [SerializeField] AudioMixer masterAudio;
    [SerializeField] JoyButton audio;
    bool audioActive = true;

    public string nivelNumber;
    // Start is called before the first frame update
    void Start()
    {
        EstadoJuego.estadoJuego.buscarObjetosPorScene();
        
        //Fetch the Event System from the Scene
        m_EventSystem = FindObjectOfType<EventSystem>();

        camera = Camera.main;
        Time.timeScale = 1;
    }
    private void Update()
    {
        AudioListen();
        menuBotones();
        niveles();
    }

    private void menuBotones()
    {
        
        if (inicioJoy.Pressed)
        {
            
            MenuInicioGO.SetActive(true);
            ui.gameObject.SetActive(false);
            
            //salirBoton.gameObject.SetActive(true);
            inicioJoy.Pressed = false;
        }
        if (opcionesJoy.Pressed)
        {
            ui.gameObject.SetActive(false);
            opcionesGO.SetActive(true);
            MenuInicioGO.SetActive(false);
            salirBoton.gameObject.SetActive(true);
            opcionesJoy.Pressed = false;
        }
        if (creditos.Pressed) {
            creditos.Pressed = false;
            credits.SetActive(true);

        }
        if (salirJoy.Pressed)
        {
            salirJoy.Pressed = false;
            //Application.Quit();
            salirBoton.gameObject.SetActive(true);
            memuUI.SetActive(false);
            tiendaComprar.SetActive(true);
            //GetComponent<Menu>().enabled = false;
        }
        if (salirBoton.Pressed)
        {
            credits.SetActive(false);
            salirBoton.gameObject.SetActive(false);
            ui.gameObject.SetActive(true);
            opcionesGO.SetActive(false);
            MenuInicioGO.SetActive(false);
            memuUI.SetActive(true);
            tiendaComprar.SetActive(false);
            salirBoton.Pressed = false;
            gameObject.GetComponent<Items>().devolverValores();
            itemsMenu.SetActive(false);
        }
        if (helic.Pressed) {
            helic.Pressed = false;
            helicTienda.SetActive(true);
            memuUI.SetActive(false);
            tiendaComprar.SetActive(false);
            GetComponent<Menu>().enabled = false;
            fondo.SetActive(false);
            helicTienda.GetComponent<TiendaObj>().cambioDeTexto();
            salirBoton.gameObject.SetActive(false);
        }
        if (JoyTieVida.Pressed) {
            JoyTieVida.Pressed = false;
            tiendaVida.SetActive(true);
            memuUI.SetActive(false);
            tiendaComprar.SetActive(false);
            GetComponent<Menu>().enabled = false;
            //activo los textos de la tienda
            gameObject.GetComponent<Items>().actualizarVidasTienda();

            salirBoton.gameObject.SetActive(false);
        }
    }
    void niveles() {
        if (nivel1[0].Pressed) {
            nivelNumber = "1";
            InicializarMenuNivel();
        }
        if (nivel1[1].Pressed) {
            nivelNumber = "2";
            InicializarMenuNivel();
        }
        if (nivel1[2].Pressed)
        {
            nivelNumber = "3";
            InicializarMenuNivel();
        }

    }
    public void NivelesEntry(int NivelNivel) {
        nivelNumber = NivelNivel.ToString();
        InicializarMenuNivel();

    }
    void InicializarMenuNivel() {
        itemsMenu.SetActive(true);
        gameObject.GetComponent<Items>().actualizarVidasNivel();
        gameObject.GetComponent<Items>().actualizarPorCadaClic();
    }
    private void AudioListen()
    {
        if (audio.Pressed && audioActive)
        {
            masterAudio.SetFloat("Master", -80);
            print("audio listen");
            audioActive = false;
            audio.Pressed = false;
            EstadoJuego.estadoJuego.audioPrendido = false;
        }
        else if (audio.Pressed && !audioActive)
        {
            audioActive = true;
            masterAudio.SetFloat("Master", 0);
            print("audio listen");
            audio.Pressed = false;
            EstadoJuego.estadoJuego.audioPrendido = true;
        }
    }
}
