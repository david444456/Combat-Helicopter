using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiendaObj : MonoBehaviour
{
    
    [SerializeField] GameObject tienda;
    [SerializeField] JoyButton derecha;
    [SerializeField] JoyButton izquierda;
    [SerializeField] GameObject GOPrincipal;
    [SerializeField] float movementPeriod = 1;
   
    bool puedoMoverme = true;
    int contadorObj = 0;
    string CambioEnEltexto;

    [Header("Esto es la UIMenu")]
    [SerializeField] Text textCompra;
    [SerializeField] GameObject uiMenu;
    [SerializeField] JoyButton salir;
    [SerializeField] Menu menu;
    [SerializeField] GameObject tiendaMenu;
    [SerializeField] GameObject fondo;
    [SerializeField] GameObject salirboton;
    // Start is called before the first frame update
    void Start()
    {
        cambioDeTexto();
        contadorObj = 0;
        Time.timeScale = 1;
    }
    public void elegirBoton() {
        if (contadorObj == 1) {

            EstadoJuego.estadoJuego.numeroHelic = contadorObj;
            return;
        }
        if (contadorObj == 0 )
        {
            if (EstadoJuego.estadoJuego.helicVerde == true)
            {
                EstadoJuego.estadoJuego.numeroHelic = contadorObj;
               

                print("elegi" + contadorObj);
            }
            else if (EstadoJuego.estadoJuego.ValorMoneda >= 1000) {
                EstadoJuego.estadoJuego.incrementarValor(-1000);
                EstadoJuego.estadoJuego.helicVerde = true;
                EstadoJuego.estadoJuego.Helicopter = 1;
                EstadoJuego.estadoJuego.Guardar();
                Camera.main.gameObject.GetComponent<Items>().SonidoCompra();
                cambioDeTexto();
            }
            else
            {
                SSTools.ShowMessage(" There are not enough coins ", SSTools.Position.bottom, SSTools.Timee.oneSecond);
                
            }
        }
        else if (contadorObj == 2)
        {
            if (EstadoJuego.estadoJuego.helicRojo == true)
            {
                EstadoJuego.estadoJuego.numeroHelic = contadorObj;
                print("elegi" + contadorObj);
            }
            else if (EstadoJuego.estadoJuego.ValorMoneda >= 1000)
            {
                EstadoJuego.estadoJuego.incrementarValor(-1000);
                EstadoJuego.estadoJuego.helicRojo = true;
                EstadoJuego.estadoJuego.Helicopter = 2;
                EstadoJuego.estadoJuego.Guardar();
                Camera.main.gameObject.GetComponent<Items>().SonidoCompra();
                cambioDeTexto();
            }
            else {
                SSTools.ShowMessage(" There are not enough coins ", SSTools.Position.bottom, SSTools.Timee.oneSecond);
                
            }
        }
 
        
    }
    // Update is called once per frame
    void Update()
    {
        if (derecha.Pressed && puedoMoverme) {
            derecha.Pressed = false;
            StartCoroutine(tranformPositionDer());
        }
        if (izquierda.Pressed && puedoMoverme)
        {
            izquierda.Pressed = false;
            StartCoroutine(tranformPositionizq());
        }
        if (salir.Pressed) {
            salir.Pressed = false;
            uiMenu.SetActive(true);
            menu.enabled = true;
            tiendaMenu.SetActive(true);
            gameObject.SetActive(false);
            fondo.SetActive(true);
            salirboton.SetActive(true);
            GOPrincipal.transform.position = new Vector3(0, GOPrincipal.transform.position.y, GOPrincipal.transform.position.z);
            
            EstadoJuego.estadoJuego.Guardar();
            contadorObj = 0;
        }
    }

   public void cambioDeTexto() {

        if (contadorObj == 1)
        {
            textCompra.text = "select";
            
            return;
        }
        if (contadorObj == 0 && EstadoJuego.estadoJuego.helicVerde == true)
        {

            textCompra.text = "select";

        } else if (contadorObj == 2 && EstadoJuego.estadoJuego.helicRojo == true) {
            textCompra.text = "select";
        }
        else {
            
            textCompra.text = "1000M";
            
        }
    }

    IEnumerator tranformPositionDer (){
        switch (contadorObj) {
            case 0:
            case 1:
                float t = 0;
                puedoMoverme = false;
                Vector3 pos = GOPrincipal.transform.position + new Vector3(-20, 0, 0);
                while (t <1)
                {
                    t += Time.deltaTime / movementPeriod;
                    GOPrincipal.transform.position = Vector3.Lerp(GOPrincipal.transform.position, pos, movementPeriod);
                    
                    yield return null;
                }
                
                contadorObj++;
                cambioDeTexto();
                puedoMoverme = true;
                break;    
        }
        
    }
    IEnumerator tranformPositionizq()
    {
        switch (contadorObj)
        {
            case 1:
            case 2:
                float t = 0;
                puedoMoverme = false;
                Vector3 pos = GOPrincipal.transform.position + new Vector3(20, 0, 0);
                while (t < 1)
                {
                    t += Time.deltaTime / movementPeriod;
                    GOPrincipal.transform.position = Vector3.Lerp(GOPrincipal.transform.position, pos, movementPeriod);

                    yield return null;
                }
                
                contadorObj--;
                cambioDeTexto();
                puedoMoverme = true;
                break;
        }

    }
}
