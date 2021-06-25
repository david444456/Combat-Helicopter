using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDaño : MonoBehaviour
{
    [Header("GO")]
    [SerializeField] GameObject Bomba;
    [SerializeField] GameObject misil;
    [SerializeField] GameObject posMisil1;
    [SerializeField] GameObject posMisil2;
    [SerializeField] GameObject partycleBalas;
    public GameObject platform;
    [SerializeField] Vector3 correccionCreacionBomba;
    [SerializeField] Vector3 correccionRespawn;
    [SerializeField] ParticleSystem deadHelic;

    [Header("JoyButton")]
    public JoyButton buttonAttack;
    public JoyButton Arma2;
    public JoyButton Arma3;
    public Slider sliBomb;
    [Header("Mas")]
    [SerializeField] float vidaHelic = 10;
    float vihelic2;
    [SerializeField] float secondsBetweenSpawns;
    public GameObject SliderVida;
    [SerializeField] ParticleSystem efectHitTower;
    public GameObject textVida;
    public int puntosVida;
    public Text puntosVidaText;
    [SerializeField] AudioClip clipHit;
    [SerializeField] AudioSource audios;
    [SerializeField] AudioClip bombDead;
    Transform[] trans;
    [Header("Armas")]

    float BombaRE = 5;
    float timeArma2RE = 4;
    float timeArma3RE = 2;
    float timeBomba;
    float timeArma2;
    float timeArma3;

    float t;

    private bool m_die = false;

    public bool Die{
        get => m_die;
        private set => m_die = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(puntosVida);
        puntosVida = puntosVida + EstadoJuego.estadoJuego.vidasNivel;

        EstadoJuego.estadoJuego.vidasComienzoHelic = puntosVida;
        if (EstadoJuego.estadoJuego.RENivel) {
            BombaRE = 6 / 2;
            timeArma2RE = 4 / 2;
            timeArma3RE = 2 / 2;
            sliBomb.maxValue = BombaRE;
            Arma2.gameObject.GetComponent<Slider>().maxValue = timeArma2RE;
            Arma3.gameObject.GetComponent<Slider>().maxValue = timeArma3RE;
        }
        //prueba llamando a 
        SliderVida.GetComponent<Slider>().maxValue = vidaHelic;
        SliderVida.GetComponent<Slider>().value = vidaHelic;
        vihelic2 = vidaHelic;
        puntosVidaText.text = puntosVida.ToString();
    }


    // Update is called once per frame
    void Update()
    {
       
        if (textVida.gameObject.activeSelf)
        {
            t += Time.deltaTime;
            textVida.gameObject.GetComponent<Slider>().value = t;
          
        }
        if (GetComponent<HelicopterController>().isActiveAndEnabled) {
            verificacionArmas();
        }
    }

    void verificacionArmas() {

        timeArma3 = Time.deltaTime + timeArma3;
        Arma3.gameObject.GetComponent<Slider>().value = timeArma3;

        timeArma2 = Time.deltaTime + timeArma2;
        Arma2.gameObject.GetComponent<Slider>().value = timeArma2;

        timeBomba = Time.deltaTime + timeBomba;
        sliBomb.value = timeBomba;


        if (Arma3.Pressed && timeArma3 > timeArma3RE)
        {
            Instantiate(partycleBalas, transform.position + correccionCreacionBomba, Quaternion.identity);
            timeArma3 = 0;
        }
        if (Arma2.Pressed && timeArma2 > timeArma2RE)
        {
  
           
            var misilGO1 = Instantiate(misil,posMisil1.transform.position , transform.rotation);
            var misilGO2 = Instantiate(misil,posMisil2.transform.position , transform.rotation);
            timeArma2 = 0;
            
            Destroy(misilGO1, 12f);
            Destroy(misilGO1, 12f);
        }
        if (buttonAttack.Pressed && timeBomba > BombaRE)
        {
            
            Instantiate(Bomba, transform.position + correccionCreacionBomba, Quaternion.identity);
            timeBomba = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<HelicopterController>().isActiveAndEnabled)
        {
            if (collision.gameObject.tag == "Bomba")
            {
                return;
            }
            if (collision.gameObject.tag == "Enemies") {
                var EnemySpawn = FindObjectOfType<EnemySpawner>();
                EnemySpawn.EnemyVivos--;
                EnemySpawn.spawnedEnemies.text = (EnemySpawn.EnemyVivos + 1).ToString();
                Destroy(collision.gameObject);
                
            }
            if (collision.gameObject.tag == "Torres")
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Respawn")
            {
                return;
            }

            Die = true;
            EffectParty(deadHelic, transform);
            desactivarHeli(false);
            audios.clip = bombDead;
            audios.Play();
            StartCoroutine(RespawnTiempo());
        }
    }

    IEnumerator RespawnTiempo()
    {
        puntosVida--;
        puntosVidaText.text = puntosVida.ToString();
        if (puntosVida <= 0)
        {
            audios.clip = bombDead;
            audios.Play();
            FindObjectOfType<PlayerHealth>().Derrota();
        }
        yield return new WaitForSeconds(secondsBetweenSpawns);
        desactivarHeli(true);

        Die = false;

        gameObject.transform.position = platform.transform.position + correccionRespawn;
        SliderVida.GetComponent<Slider>().value = vidaHelic;
    }

    void desactivarHeli(bool valorLogico)
    {
        textVida.gameObject.SetActive(!valorLogico);
        vidaHelic = vihelic2;
        
        if (!valorLogico)
        {
            trans = GetComponentsInChildren<Transform>();
        }

        foreach (Transform t in trans)
        {
            if (!(t.gameObject.name == this.gameObject.name))
            {
                t.gameObject.SetActive(valorLogico);
            }
        }
        t = 0;
        GetComponent<CapsuleCollider>().enabled = valorLogico;
        GetComponent<HelicopterController>().enabled = valorLogico;
    }

    private void OnParticleCollision(GameObject other)
    {

        if (other.name == "Bullets") {
            audios.clip = clipHit;
            audios.Play();
            vidaHelic--;
            EffectParty(efectHitTower, transform);
            SliderVida.GetComponent<Slider>().value = vidaHelic;
            
            if (vidaHelic <= 0) {
                desactivarHeli(false);
                EffectParty(deadHelic, transform);
                StartCoroutine(RespawnTiempo());
            }
            

        }
    }

    void EffectParty(ParticleSystem effect, Transform pos) {
        var vfx = Instantiate(effect, pos.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay2 = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay2);
    }
}
