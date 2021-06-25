using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSRewardedVideo : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] GameObject menuContinue;

    string GooglePlay_Id = "3119158";
    string placementID = "rewardedVideo"; //ID del anuncio
    public Text txtMessage;
    public Text txtGemns;
    public static bool diRecompensa = false;

    [Range(0, 10)] public int rewardGemns;
    int gemns;

    void Start()
    {
        // Inicia el SDK de Unity Ads
        Advertisement.AddListener(this);
        Advertisement.Initialize (GooglePlay_Id); //El TRUE es para activar el Modo Testeo
        //Advertisement.Initialize(placementID);

        //Setea las gemas y el texto a CERO
        gemns = 0;
        //txtGemns.text = gemns.ToString();
    }

    public void ContinueLife() {
        menuContinue.SetActive(false);
        FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>().vuelveAlaVida();
    }

    public void ShowRewardedVideo()
    {

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {

        }
        else
        {
            txtMessage.text = " You have \n no \n internet";
            return;
        }

        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(placementID) && !diRecompensa)
        {
            Advertisement.Show(placementID);
            print("REWARDED - Video abierto.");
        }
        else
        {
            print("El Video Recompensado aun no esta listo. " + diRecompensa);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            //ganar
            if (EstadoJuego.estadoJuego.IfIWin)
            {
                EstadoJuego.estadoJuego.incrementarValor(EstadoJuego.estadoJuego.DateAdsValueX2);
            }
            else
            {
                menuContinue.SetActive(true);
            }
            diRecompensa = true;
            print("Di recompensa");
        }
        else if (showResult == ShowResult.Skipped)
        {
            print("REWARDED - Video salteado.");
            //txtMessage.text = "REWARDED - Video salteado.";
        }
        else if (showResult == ShowResult.Failed)
        {
            print("REWARDED - Falla al cargar el video.");
            //txtMessage.text = "REWARDED - Falla al cargar el video.";
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == placementID)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
