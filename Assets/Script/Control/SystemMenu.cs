using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SystemMenu : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] GameObject backGroundLevel;
    [SerializeField] Image[] imagesLevels = null;
    [SerializeField] Sprite spriteBackGroundUnlock;

    private int valueLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        EstadoJuego.estadoJuego.CargarNivelNumero();

        valueLevel = EstadoJuego.estadoJuego.GetScoreLevel();
        valueLevel = valueLevel - 15;
        if (valueLevel <= 0) valueLevel = 0;
    }

    public void activeLevelSelectionBackGround(bool boolBackGround) {
        backGroundLevel.SetActive(boolBackGround);

        if (boolBackGround) {
            verifyAllLevelsUnlock();
        }
    }

    void verifyAllLevelsUnlock() {
        print("Value level: " + valueLevel);
        for (int i = 0; i < valueLevel; i++) {
            imagesLevels[i].sprite = spriteBackGroundUnlock;
        }
    }

    public void ButtonTestAugmentLevel() {
        EstadoJuego.estadoJuego.GuardarNivel(15);
    }
}
