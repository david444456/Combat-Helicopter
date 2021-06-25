using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaVida : MonoBehaviour
{

    [SerializeField] GameObject tienda;
    [SerializeField] JoyButton vidas;
    [SerializeField] JoyButton RE;
    [SerializeField] JoyButton VidasAliado;

    [Header("Esto es la UIMenu")]
    [SerializeField] GameObject uiMenu;
    [SerializeField] JoyButton salir;
    [SerializeField] Menu menu;
    [SerializeField] GameObject tiendaMenu;
    [SerializeField] GameObject fondo;
    [SerializeField] GameObject salirboton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RE.Pressed )
        {
            RE.Pressed = false;
            print("RE");
        }
        if (vidas.Pressed)
        {
            vidas.Pressed = false;
            print("Vidas ");
        }
        if (VidasAliado.Pressed)
        {
            VidasAliado.Pressed = false;
            print("Vidas Aliado");

        }
        if (salir.Pressed)
        {
            salir.Pressed = false;
           // uiMenu.SetActive(true);
            menu.enabled = true;
            tiendaMenu.SetActive(true);
            gameObject.SetActive(false);
            fondo.SetActive(true);
            salirboton.SetActive(true);
        }
    }
}
