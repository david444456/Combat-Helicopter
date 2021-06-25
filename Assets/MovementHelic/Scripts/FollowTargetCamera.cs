using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowTargetCamera : MonoBehaviour
{
    [Header("HEL")]
    public GameObject helic1;
    public GameObject helic2;
    public GameObject helic3;
    public GameObject platform;
    [SerializeField] ControlPanel controlPanel;
    
    [SerializeField] JoyButton up;
    [SerializeField] JoyButton down;
    [SerializeField] JoyButton izq;
    [SerializeField] JoyButton Der;
    [SerializeField] Text Engine;
    [SerializeField] JoyButton ButtonAttack;
    [SerializeField] JoyButton Arma2;
    [SerializeField] JoyButton Arma3;
    [SerializeField] Slider sliBomb;
    [SerializeField] GameObject Vida;
    [SerializeField] GameObject VidaRespawn;
    [SerializeField] Text PuntosVidas;
    [SerializeField] int vidaHelic;
    HelicopterController hel;

    [Header("Normal")]
    [SerializeField] Vector3 correccionZ;

    


    public Transform Target;
    public float PositionFolowForce = 5f;
    public float RotationFolowForce = 5f;
    [SerializeField] Camera camera;
    [SerializeField]LayerMask mask;

    private void Awake()
    {
        
    }

    void ponerDatos(GameObject Hel) {
        HelicopterController HelCont = Hel.gameObject.GetComponent<HelicopterController>();
        HelCont.ControlPanel = controlPanel;
        HelCont.upButton = up;
        HelCont.downButton = down;
        HelCont.izq = izq;
        HelCont.der = Der;
        HelCont.engine = Engine;
        PlayerDaño plDaño = Hel.gameObject.GetComponent<PlayerDaño>();
        plDaño.platform = platform;
        plDaño.buttonAttack = ButtonAttack;
        plDaño.Arma2 = Arma2;
        plDaño.Arma3 = Arma3;
        plDaño.sliBomb = sliBomb;
        plDaño.SliderVida = Vida;
        plDaño.textVida = VidaRespawn;
        plDaño.puntosVidaText = PuntosVidas;
        plDaño.puntosVida = vidaHelic;

    }
    void Start ()
	{
        switch (EstadoJuego.estadoJuego.numeroHelic)
        {
            case 0:
                GameObject Hel = Instantiate(helic1, platform.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                Target = Hel.transform;
                hel = Hel.GetComponent<HelicopterController>();
                ponerDatos(Hel);
                break;
            case 1:
                GameObject Hel2 = Instantiate(helic2, platform.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                Target = Hel2.transform;
                hel = Hel2.GetComponent<HelicopterController>();
                ponerDatos(Hel2);
                break;
            case 2:
                GameObject Hel3 = Instantiate(helic3, platform.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                Target = Hel3.transform;
                hel = Hel3.GetComponent<HelicopterController>();
                ponerDatos(Hel3);
                break;
        }

        //mask = 0 << LayerMask.NameToLayer("NoSeVe") | 0 << 9;
    }

    void FixedUpdate()
    {
        float distance = 1;
        var vector = Vector3.forward;
        var dir = Target.rotation * Vector3.forward;
        dir.y = 0f;
        if (dir.magnitude > 0f) vector = dir / dir.magnitude;

        //mio
        RaycastHit hit;

        if (Physics.Linecast(camera.transform.position, Target.transform.position, out hit, mask))
        {
            Debug.Log(" " + hit.distance + " " + hit.transform.name);
            print("Esta");
            camera.nearClipPlane = hit.distance + 10;
        }
        else {
            camera.nearClipPlane = 1;
        }
        //


        transform.position = Vector3.Lerp(transform.position, Target.position, (PositionFolowForce * Time.deltaTime));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vector), Time.deltaTime);

    }

    public void Up()
    {
        hel.EngineForce += 0.1f;
    }
    public void Down()
    {
        hel.EngineForce -= 0.19f;
    }
}

