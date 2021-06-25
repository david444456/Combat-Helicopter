using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] EnemyMovement Patrulla;
    [SerializeField] GameObject transformParentEnemy;
    public Text spawnedEnemies;
    [SerializeField] AudioClip spawnEnemySFX;
    [SerializeField] AudioSource audioSourceDefeat;
    [SerializeField] AudioClip victoryClip;   

    List<GameObject> enemyMv = new List<GameObject>();

    public int score;
    [SerializeField] int autosChiquitos;
    [SerializeField] GameObject primerAuto;

    [HideInInspector] public int EnemyVivos;
    public float movementEnemy;
    
    bool soloUnaVez = true;

    [SerializeField] GameObject lastEnemy;

    [Header("Efecto de particulas")]
    [SerializeField] float EmisionParticula;
    [SerializeField] float VelocidadParticula;
    


    [Header("Ganaste")]
    [SerializeField] GameObject ganasteMenu;
   // [SerializeField] int numeroDenivelSig;
    private void Awake()
    {
        Debug.Log(score);
    }

    void Start()
    {

        EnemyVivos = score;
        StartCoroutine(RepeatedlySpawnEnemies());
        spawnedEnemies.text = (EnemyVivos + 1).ToString();
        BuscarTorres();
    }
    void BuscarTorres() {
        var tower = FindObjectsOfType<Tower>();
        foreach (Tower t in tower) {
            t.setPartycle(VelocidadParticula, EmisionParticula);
        }
    }
    private void Update()
    {
        if (lastEnemy != null)
        {
            if (lastEnemy.activeSelf && lastEnemy.gameObject.tag == "Patrulla")
            {
                if (lastEnemy.gameObject.GetComponent<EnemyMovement>().llegueAlFinal && soloUnaVez)
                {
                    StartCoroutine(repeatList());
                    soloUnaVez = false;
                }
            }
        }
        if (EnemyVivos <= -1) {
            // llamar a ganaste? hay otro mas abajo
            EstadoJuego.estadoJuego.GuardarNivel(EstadoJuego.estadoJuego.nombreSceneSig);
            Invoke("ganaste", 1);
            
          
            
        }
    }
    
    IEnumerator RepeatedlySpawnEnemies()
    {
        while (score > 0)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            if (score > 0)
            {
                AddScore(-1);
                //aqui tendria que poder ver cuantos autos quiero de uno y otro y instanciarlos,
                if (autosChiquitos > 0)
                {
                    //instanciar el primer auto, 
                    lastEnemy = Instantiate(primerAuto, transform.position, Quaternion.identity, transformParentEnemy.transform).gameObject;
                    autosChiquitos--;
                }
                else
                {
                    lastEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transformParentEnemy.transform).gameObject;
                }
                GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            }
        }
        if (score == 0)
        {
            lastEnemy = Instantiate(Patrulla, transform.position, Quaternion.identity, transformParentEnemy.transform).gameObject;
        }


    }
    IEnumerator repeatList() {
        
        for (int i = 0; i < enemyMv.Count; i++)
        {
            if (score >= 0)
            {
                
                //AddScore(1);
                lastEnemy = enemyMv[i].gameObject;
                lastEnemy.transform.position = transform.position;
                lastEnemy.GetComponent<EnemyMovement>().StartCou();

                lastEnemy.GetComponent<EnemyMovement>().body.SetActive(true);
                
                
                lastEnemy.GetComponent<EnemyMovement>().llegueAlFinal = false;
                GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
                
                yield return new WaitForSeconds(secondsBetweenSpawns);
            }
        }
        int cantidadList = enemyMv.Count;
        for (int i = 0; i < cantidadList; i++)
        {
            enemyMv.Remove(enemyMv[0].gameObject);
        }
        soloUnaVez = true;
        print("despues del for");

        var obj = FindObjectsOfType<EnemyMovement>();

        if (obj.Length == 1) {
            // llamar a victoria!
            EstadoJuego.estadoJuego.GuardarNivel(1);

            print("Ganaste una verga");
            Invoke("ganaste", 1);
            
            
        }
    }
    void ganaste() {
        //UnityADSRewardedVideo.diRecompensa = false;
        audioSourceDefeat.clip = victoryClip;
        audioSourceDefeat.Play();
        EstadoJuego.estadoJuego.IfIWin = true;
        ganasteMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private void AddScore(int intt)
    {
        score+= intt;
        spawnedEnemies.text = (EnemyVivos+1).ToString();
        if (score <= -1) {
            print("Perdiste tus coches");
        }
    }
    public void agregarLista(GameObject em) {
        enemyMv.Add(em);
    }
}
