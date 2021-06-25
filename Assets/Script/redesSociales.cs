using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redesSociales : MonoBehaviour
{
    public void Youtube() {
        Application.OpenURL("https://www.youtube.com/channel/UCxSk7V_w1bQqd0WJGVGaVHw?view_as=subscriber");

    }
    public void Instagram() {
        Application.OpenURL("https://www.instagram.com/td.tidigames/");
    }
    public void AumentarCompra() {
        EstadoJuego.estadoJuego.incrementarValor(1000);
    }

}
