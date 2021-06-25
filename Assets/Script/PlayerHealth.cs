using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] int puntosHelic;
    [SerializeField] public int EsceneSig;
    [SerializeField] Text HealtText;
    [SerializeField] AudioClip playerDamageSFX;

    [Header("Menu")]
    [SerializeField] JoyButton menu;
    [SerializeField] JoyButton salirMenu;
    [SerializeField] JoyButton resume;
    [SerializeField] GameObject menuGO;

    [Header("Menu Perdiste")]
    [SerializeField] GameObject menuPerdiste;
    [SerializeField] JoyButton restart;
    [SerializeField] JoyButton salir;
    [SerializeField] JoyButton verVideoVidas;
    [SerializeField] AudioClip defeatClip;
    
    //[SerializeField] string nombreSceneActual = "2";

    [Header("Menu Ganaste")]
    [SerializeField] GameObject menuGanaste;
    [SerializeField] JoyButton restart2;
    [SerializeField] JoyButton salir2;
    [SerializeField] public JoyButton continu;
    [SerializeField] AudioClip audioTouch;
    [SerializeField] AudioSource audios;

    [SerializeField] AudioSource audiosDefeat;
    [SerializeField] GameObject vidaSli;
    //[SerializeField] string nombreSceneSiguiente = "2";
    private void Start()
    {
        UnityADSRewardedVideo.diRecompensa = false;


        EstadoJuego.estadoJuego.AnunciosPorNivel++;
        print("Anuncios " + EstadoJuego.estadoJuego.AnunciosPorNivel);
        EstadoJuego.estadoJuego.vidasNivel += puntosHelic;
        health = EstadoJuego.estadoJuego.vidasBase + health;
        EstadoJuego.estadoJuego.vidasComienzoBase = health;
        Time.timeScale = 1;
        EstadoJuego.estadoJuego.buscarObjetosPorScene();
        HealtText.text = health.ToString();
    }

    private void Update()
    {
        HealtText.text = health.ToString();
        perdisteyganaste();
    }
    void perdisteyganaste() {
        if (menu.Pressed && !menuGO.activeSelf)
        {
            audios.clip = audioTouch;
            audios.Play();
            menuGO.SetActive(true);
            salirMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (salirMenu.Pressed || resume.Pressed)
        {
            audios.clip = audioTouch;
            audios.Play();
            menuGO.SetActive(false);
            Time.timeScale = 1;
            salirMenu.gameObject.SetActive(false);
            salirMenu.Pressed = false;
            resume.Pressed = false;
        }
        if (menuPerdiste.activeSelf)
        {
            if (restart.Pressed)
            {
                /*if (EstadoJuego.estadoJuego.AnunciosPorNivel >= 2)
                {
                    //play ad
                    var ad = FindObjectOfType<UnityADSRewardedVideo>(); 
                    ad.GetComponent<UnityADSRewardedVideo>().ShowRewardedVideo();
                    EstadoJuego.estadoJuego.AnunciosPorNivel = 0;
                    restart.Pressed = false;
                    return;
                }*/
                UnityADSRewardedVideo.diRecompensa = false;

                audios.clip = audioTouch;
                audios.Play();
                restart.Pressed = false;
                menu.Pressed = false;
                Time.timeScale = 1;
                EstadoJuego.estadoJuego.vidasNivel = 0;
                SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneActual);
            }
            if (salir.Pressed)
            {
                audios.clip = audioTouch;
                audios.Play();
                salir.Pressed = false;
                Time.timeScale = 1;
                //esto es para que al salir no tengas mas las vidas extra
                EstadoJuego.estadoJuego.resetVidasyRePorNivel();
                SceneManager.LoadScene("Menu");
                
            }
           /* if (verVideoVidas.Pressed)
            {
                audios.clip = audioTouch;
                audios.Play();
                verVideoVidas.Pressed = false;
                print("video amego");
            }*/
        }
        if (menuGanaste.activeSelf)
        {
            if (restart2.Pressed)
            {
                audios.clip = audioTouch;
                audios.Play();
                restart2.Pressed = false;
                menu.Pressed = false;
                Time.timeScale = 1;
                EstadoJuego.estadoJuego.vidasNivel = 0;
                SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneActual);
            }
            if (salir2.Pressed)
            {
                audios.clip = audioTouch;
                audios.Play();
                salir2.Pressed = false;
                Time.timeScale = 1;
                EstadoJuego.estadoJuego.resetVidasyRePorNivel();
                SceneManager.LoadScene("Menu");
            }
            if (continu.Pressed)
            {
                // if he win then:
                if (EstadoJuego.estadoJuego.nombreSceneActual == "20") {
                    
                    menuGanaste.transform.Find("FondoFinal").gameObject.SetActive(true);
                    //Invoke("GanasteNivelFinal", 6);
                    return;
                }

                // if "anuncios" == 4 play ad
                if (EstadoJuego.estadoJuego.AnunciosPorNivel >= 4) {
                    //play ad
                    var ad = FindObjectOfType<Banner>();
                    ad.GetComponent<Banner>().ShowInterstitial();
                    EstadoJuego.estadoJuego.AnunciosPorNivel = 0;
                    EstadoJuego.estadoJuego.AnunciosBoolNivel = true;
                    if (!UnityADSRewardedVideo.diRecompensa)
                    {
                        continu.Pressed = false;
                        
                    }
                    return;
                }

                audios.clip = audioTouch;
                audios.Play();
                continu.Pressed = false;
                EstadoJuego.estadoJuego.resetVidasyRePorNivel();
                //scene
                SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneSig.ToString());

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        health = health - healthDecrease;
        HealtText.text = health.ToString();
        if (health <= 0)
        {
            Derrota();
        }
        
    }
   public void GanasteNivelFinal() {
        print("Me llamo para volver al menu");
        EstadoJuego.estadoJuego.resetVidasyRePorNivel();
        SceneManager.LoadScene("Menu");
    }
    public void Derrota() {
        vidaSli.SetActive(false);
        Invoke("derrota2", 0.3f);
        
    }
    void derrota2() {

        audiosDefeat.Play();
        menuPerdiste.SetActive(true);
        print("Perdiste");
        EstadoJuego.estadoJuego.vivoHelicONo = false;
        Time.timeScale = 0;
    }

    public void vuelveAlaVida() {

        //datos


        var plHeal = FindObjectOfType<PlayerDaño>();

        print(plHeal.puntosVida);
        if ((plHeal.puntosVida != 0)) {
            vidaSli.SetActive(false);
            print("Es ugyak a cero ameogogog" + vidaSli.activeSelf);
        }
        if (plHeal.puntosVida == 0)
        {
            plHeal.textVida.gameObject.SetActive(true);
        }
        plHeal.puntosVida = EstadoJuego.estadoJuego.vidasComienzoHelic;
        health = EstadoJuego.estadoJuego.vidasComienzoBase;
        
        plHeal.puntosVidaText.text = plHeal.puntosVida.ToString();

        //algo: ads esta en el ganaste de enemySpawner
        //UnityADSRewardedVideo.diRecompensa = false;

        menuPerdiste.SetActive(false);
        print("Volviste a la vida loquito" + vidaSli.activeSelf);
        EstadoJuego.estadoJuego.vivoHelicONo = true;
        Time.timeScale = 1;
    }

}
