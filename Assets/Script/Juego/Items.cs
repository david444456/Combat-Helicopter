using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Notifications.Android;

public class Items : MonoBehaviour
{
    public static Items items;

    [Header("VidasTienda")]
    [SerializeField] Text vidasT;
    [SerializeField] Text RET;
    [SerializeField] Text vidasBaseT;
    [SerializeField] AudioClip touchVidas;

    [Header("Vidas principal Menu")]
    [SerializeField] Text vidasMenu;
    [SerializeField] Text REMEnu;
    [SerializeField] Text vidaBaseMEnu;

    //re
    [SerializeField] GameObject okImage;
    [SerializeField] bool seTocoOk;


    [SerializeField] AudioSource audioSource;

    [Header("textos menu 2")]
    [SerializeField] Text vidasMAs;
    
    [SerializeField] Text vidaBaseMAS;

    public int vidasMasInt = 0;
    public int vidasValor;

    public int baseValor = 0;
    public int vidabaseMasInt = 0;

    

    public void actualizarPorCadaClic() {
        vidasValor = EstadoJuego.estadoJuego.valorVida;
        baseValor = EstadoJuego.estadoJuego.valorVidaBase;
    }

    public void actualizarOk() {
        seTocoOk = !seTocoOk;

        
        if (seTocoOk)
        {
            if (EstadoJuego.estadoJuego.valorRE > 0)
            {

                REMEnu.text = (EstadoJuego.estadoJuego.valorRE - 1).ToString();
                okImage.SetActive(seTocoOk);
            }
            else {
                SSTools.ShowMessage(" You don't have, go to the store", SSTools.Position.bottom, SSTools.Timee.oneSecond);
            }
        }
        else {
            okImage.SetActive(seTocoOk);
            REMEnu.text = (EstadoJuego.estadoJuego.valorRE).ToString();
        }
    }

    void Start()
    {
        vidasMAs.text = "0";
        
        vidaBaseMAS.text = "0";
        vidasValor = EstadoJuego.estadoJuego.valorVida;
        actualizarVidasTienda();
    }
    public void plataExtra() {
        EstadoJuego.estadoJuego.incrementarValor(100);
    }
    public void aumentarBaseText(int t)
    {
        if (t <= 0)
        {
            if (vidabaseMasInt > 0)
            {
                baseValor++;
                vidabaseMasInt = vidabaseMasInt + t;
                vidaBaseMAS.text = vidabaseMasInt.ToString();
            }
        }
        else if (t >= 0)
        {
            if (baseValor > 0)
            {
                baseValor--;
                vidabaseMasInt = vidabaseMasInt + t;
                vidaBaseMAS.text = vidabaseMasInt.ToString();
            }
            else {
                
                SSTools.ShowMessage("You don't have, go to the store", SSTools.Position.bottom, SSTools.Timee.oneSecond);
            }
        }
        vidaBaseMEnu.text = baseValor.ToString();
    }
    public void aumentarVidaTExt(int t) {
        if (t <= 0)
        {
            if (vidasMasInt > 0)
            {
                vidasValor++;
                vidasMasInt = vidasMasInt + t;
                vidasMAs.text = vidasMasInt.ToString();
            }
        }
        else if (t >= 0) {
            if (vidasValor > 0)
            {

                vidasValor--;
                vidasMasInt = vidasMasInt + t;
                vidasMAs.text = vidasMasInt.ToString();
            }
            else {
                SSTools.ShowMessage(" You don't have, go to the store", SSTools.Position.bottom, SSTools.Timee.oneSecond);
            }
        }
        vidasMenu.text = vidasValor.ToString();
    }

    public void confirmacion() {
        EstadoJuego.estadoJuego.incrementarVida(EstadoJuego.estadoJuego.valorVida -vidasMasInt);
        if (seTocoOk && EstadoJuego.estadoJuego.valorRE > 0)
        {
            EstadoJuego.estadoJuego.incrementarRE(EstadoJuego.estadoJuego.valorRE - 1);
        }
        EstadoJuego.estadoJuego.incrementarVidasBase(EstadoJuego.estadoJuego.valorVidaBase - vidabaseMasInt);

        EstadoJuego.estadoJuego.vidasNivel = vidasMasInt;
        EstadoJuego.estadoJuego.RENivel = seTocoOk;
        EstadoJuego.estadoJuego.vidasBase = vidabaseMasInt;
        SceneManager.LoadScene(GetComponent<Menu>().nivelNumber);
    }

    //este es importante, si no no se quedaria en 0

    public void devolverValores() {
        //EstadoJuego.estadoJuego.incrementarVida(EstadoJuego.estadoJuego.valorVida + vidasMasInt);
        vidasMasInt = 0;
        vidasMAs.text = vidasMasInt.ToString();
        seTocoOk = false;
        okImage.SetActive(false);
        vidabaseMasInt = 0;
        vidaBaseMAS.text = vidabaseMasInt.ToString();

    }
    public void actualizarVidasNivel() {
        vidasMenu.text = EstadoJuego.estadoJuego.valorVida.ToString();
        REMEnu.text = EstadoJuego.estadoJuego.valorRE.ToString();
        vidaBaseMEnu.text = EstadoJuego.estadoJuego.valorVidaBase.ToString();
    }
    public void actualizarVidasTienda() {
        vidasT.text = EstadoJuego.estadoJuego.valorVida.ToString();
        RET.text = EstadoJuego.estadoJuego.valorRE.ToString();
        vidasBaseT.text = EstadoJuego.estadoJuego.valorVidaBase.ToString();
    }
    public void incrementarVida() {
        
        if (EstadoJuego.estadoJuego.ValorMoneda >= 10) {
            SonidoCompra();
            EstadoJuego.estadoJuego.incrementarValor(-10);
            EstadoJuego.estadoJuego.incrementarVida(EstadoJuego.estadoJuego.valorVida + 1);
            actualizarVidasTienda();
        }
    }
    public void incrementarRE()
    {
        
        if (EstadoJuego.estadoJuego.ValorMoneda >= 50)
        {
            SonidoCompra();
            EstadoJuego.estadoJuego.incrementarValor(-50);
            EstadoJuego.estadoJuego.incrementarRE(EstadoJuego.estadoJuego.valorRE + 1);
            actualizarVidasTienda();
        }
    }
    public void incrementarVidasBase()
    {
        
        if (EstadoJuego.estadoJuego.ValorMoneda >= 100)
        {
            SonidoCompra();
            EstadoJuego.estadoJuego.incrementarValor(-100);
            EstadoJuego.estadoJuego.incrementarVidasBase(EstadoJuego.estadoJuego.valorVidaBase + 1);
            actualizarVidasTienda();
        }
    }
    public void SonidoCompra() {
        audioSource.clip = touchVidas;
        audioSource.Play();
    }
}
