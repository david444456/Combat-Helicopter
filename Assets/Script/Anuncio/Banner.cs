using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Banner : MonoBehaviour {

	string placementID = "BannerLevel"; //ID del anuncio
	public Text txtMessage;

	void Start () {
		// Inicia el SDK de Unity Ads
		//Advertisement.Initialize (placementID, true); //El TRUE es para activar el Modo Testeo
		Advertisement.Initialize (placementID);
       // ShowInterstitial();

    }

	//Muestra el Intersticial, si esta listo
	public void ShowInterstitial () {
		//ShowOptions es una coleccion que nos permite trabajar con los diferentes resultados del video
		ShowOptions options = new ShowOptions ();

		//Devolución de llamada para recibir el resultado del anuncio.
		options.resultCallback = HandleShowResult;

		//Si esta listo, muestra el video
		if (Advertisement.IsReady(placementID)) {
			Advertisement.Show (placementID, options);
			print ("INTERSTITIAL - Video abierto.");
			//txtMessage.text = "INTERSTITIAL - Video abierto.";
		} else {
            SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneSig.ToString());
            print ("El Intersticial aun no esta listo.");
			//txtMessage.text = "El Intersticial aun no esta listo.";
		}
	}

	void HandleShowResult (ShowResult result) {
		if (result == ShowResult.Finished) {
			print ("INTERSTITIAL - Video cerrado.");
            SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneSig.ToString());
            //txtMessage.text = "INTERSTITIAL - Video cerrado.";
        } else if (result == ShowResult.Skipped) {
            SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneSig.ToString());
            print ("INTERSTITIAL - Video salteado.");
			//txtMessage.text = "INTERSTITIAL - Video salteado.";
		} else if (result == ShowResult.Failed) {
            SceneManager.LoadScene(EstadoJuego.estadoJuego.nombreSceneSig.ToString());
            print ("INTERSTITIAL - Falla al cargar el video.");
			//txtMessage.text = "INTERSTITIAL - Falla al cargar el video.";
		}
	}

}
