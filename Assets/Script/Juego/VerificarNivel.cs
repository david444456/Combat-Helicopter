using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificarNivel : MonoBehaviour
{
    public GameObject[] Ig;
    public GameObject[] Joy;

    [SerializeField] Sprite sprite;
    [SerializeField] Color color;


    void Start()
    {
        
        EstadoJuego.estadoJuego.CargarNivelNumero();
        GameObject[] a;
        GameObject[] b;
        switch (EstadoJuego.estadoJuego.marcador)
        {
            case 0:
                break;
            case 1:
                Debug.Log("cargo");
                /*Ig[0].GetComponent<Image>().sprite = sprite;
                Ig[0].GetComponent<Image>().color = color;
                Ig[0].GetComponent<Image>().type = Image.Type.Sliced;
                Joy[0].SetActive(true);*/

                break;
            case 2:
                for (int j = 0; j <= 0; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                    Debug.Log("Cargo2");
                }
                
                /*a = new GameObject[] { Ig[0] };
                actualizaImagen(a);
                b = new GameObject[] { Joy[0] };
                ActualizarJoybutton(b);*/
                break;
            case 3:
                for (int j = 0; j <= 1; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                    Debug.Log("Cargo2");
                }
                /* a = new GameObject[] { Ig[0], Ig[1] };
                 actualizaImagen(a);
                  b = new GameObject[] { Joy[0], Joy[1] };
                 ActualizarJoybutton(b);*/
                break;
            case 4:
                for (int j = 0; j <= 2; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                /*a = new GameObject[] { Ig[0], Ig[1], Ig[2] };
                actualizaImagen(a);
                b = new GameObject[] { Joy[0], Joy[1] };
                ActualizarJoybutton(b);*/
                break;
            case 5:
                for (int j = 0; j <= 3; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }break;
            case 6:
                for (int j = 0; j <= 4; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 7:
                for (int j = 0; j <= 5; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 8:
                for (int j = 0; j <= 6; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 9:
                for (int j = 0; j <= 7; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 10:
                for (int j = 0; j <= 8; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 11:
                for (int j = 0; j <= 9; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 12:
                for (int j = 0; j <= 10; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 13:
                for (int j = 0; j <= 11; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 14:
                for (int j = 0; j <= 12; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 15:
                for (int j = 0; j <= 13; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
                for (int j = 0; j <= 13; j++)
                {
                    actualizaIG(Ig[j]);
                    actualizaJB(Joy[j]);
                }
                break;
        }
    }
    void actualizaIG(GameObject GO) {
        GO.GetComponent<Image>().sprite = sprite;
        GO.GetComponent<Image>().color = color;
        GO.GetComponent<Image>().type = Image.Type.Sliced;
    }
    void actualizaJB(GameObject GO) {
        GO.SetActive(true);

    }


    void actualizaImagen(GameObject[] gamesO)
    {
        
        
        foreach (GameObject GO in gamesO)
        {
            GO.GetComponent<Image>().sprite = sprite;
            GO.GetComponent<Image>().color = color;
            GO.GetComponent<Image>().type = Image.Type.Sliced;
        }
    }
    void ActualizarJoybutton(GameObject[] gamesO)
    {
        foreach (GameObject GO in gamesO)
        {
            
        }
    }
}
