
using System;
using UnityEngine;
using UnityEngine.Events;

public class ControlCallLevel : MonoBehaviour
{
    [SerializeField] EventToLoadLevel loadLevel;

    [Serializable]
    class EventToLoadLevel : UnityEvent<int> {

    }

    public void TryToLoadLevel(int index) {
        if (EstadoJuego.estadoJuego.GetScoreLevel() >= index-1) {
            loadLevel.Invoke(index);
        }
    }
}
