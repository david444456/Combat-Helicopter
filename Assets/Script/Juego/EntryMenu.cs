using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EntryMenu : MonoBehaviour
{
    [SerializeField] JoyButton restart;
    
    [SerializeField] JoyButton cambioDeLado;
    [SerializeField] JoyButton Salir;
    [SerializeField] JoyButton menu;
    [SerializeField] string nombreSceneActual = "2";
    [SerializeField] AudioClip audioTouch;
    [SerializeField] AudioSource audios;

    [Header("Cambio")]
    [SerializeField] Transform t1;
    [SerializeField] Transform t2;
    [SerializeField] Transform JoystickM;
    [SerializeField] Transform Armas;
    Transform padre1;
    Transform padre2;
    Transform transicionPadre;
    Vector3 pos1;
    Vector3 pos2;
    [Header("Audio")]
    [SerializeField] AudioMixer masterAudio;
    [SerializeField] JoyButton audio;
    bool audioActive = true;
    bool MusicActive = true;
    // Start is called before the first frame update
    void Start()
    {
        padre1 = t1.transform;
        padre2 = t2.transform;
        pos1 = JoystickM.position;
        pos2 = Armas.position;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListen();
        apretarBotones();
    }

    private void apretarBotones()
    {
        if (restart.Pressed) {

            restart.Pressed = false;
            menu.Pressed = false;
            Time.timeScale = 1;
            audios.Play();
            EstadoJuego.estadoJuego.vidasNivel = 0;
            SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneActual);
        }
        
        if (cambioDeLado.Pressed)
        {

            //modificar audio 
            if (MusicActive)
            {
                Camera.main.GetComponent<AudioSource>().volume = 0;
                MusicActive = false;
            }
            else {
                Camera.main.GetComponent<AudioSource>().volume = 1;
                MusicActive = true;
            }
            /*audios.Play();
            JoystickM.parent = padre2;
            Armas.parent = padre1;
            JoystickM.transform.position = new Vector3(pos2.x, JoystickM.transform.position.y, JoystickM.transform.position.z);
            Armas.transform.position = new Vector3(pos1.x, Armas.transform.position.y, Armas.transform.position.z) ;

            transicionPadre = padre1;
            padre1 = padre2;
            padre2 = transicionPadre;

            pos2 = Armas.position;
            pos1 = JoystickM.position;

            print("Cambio de lado");*/
            cambioDeLado.Pressed = false;
        }
        if (Salir.Pressed)
        {
            audios.Play();
            Salir.Pressed = false;
            Time.timeScale = 1;
            EstadoJuego.estadoJuego.resetVidasyRePorNivel();
            SceneManager.LoadScene("Menu");
        }
    }
    private void AudioListen() {
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
            audios.Play();
            audioActive = true;
            masterAudio.SetFloat("Master", 0);
            print("audio listen");
            audio.Pressed = false;
            EstadoJuego.estadoJuego.audioPrendido = true;
        }
    }
    
        
    
}
