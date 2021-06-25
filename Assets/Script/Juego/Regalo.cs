using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regalo : MonoBehaviour
{
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] Image image;
    [SerializeField] GameObject image2;
    [SerializeField] JoyButton joyBu;
    [SerializeField] int[] valoresMoneda;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioSource audios;
    [SerializeField]bool diRecompensa;

    public static int valoraIncrementar;
    // Start is called before the first frame update
    void Start()
    {
        valoraIncrementar = UnityEngine.Random.Range(valoresMoneda[0], valoresMoneda[1]);
        image.sprite = sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        if (joyBu.Pressed && !diRecompensa) {
            joyBu.Pressed = false;
            diRecompensa = true;
            image2.gameObject.SetActive(true);
            audios.clip = coin;
            audios.Play();
            EstadoJuego.estadoJuego.DateAdsValueX2 = valoraIncrementar;
            image2.GetComponent<Text>().text = "+"+valoraIncrementar.ToString();
            imagenVoid();
        }
    }

    private void imagenVoid()
    {
        image.sprite = sprite2;
        EstadoJuego.estadoJuego.incrementarValor(valoraIncrementar);
    }
}
