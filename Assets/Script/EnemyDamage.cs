using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeadSFX;
    [SerializeField] GameObject sliVida;
    
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
       // ProcessHit(1);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            ProcessHit(collision.gameObject.GetComponent<BombaCol>().valorHitElement);

        }
    }


    public void ProcessHit(int valorHit) {
        hitPoints -= valorHit;
 
        myAudioSource.PlayOneShot(enemyHitSFX);
        hitParticlePrefab.Play();
        sliVida.gameObject.GetComponent<Slider>().value = hitPoints;
    }

    void KillEnemy() {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        vfx.Play();
        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);
        if (EstadoJuego.estadoJuego.audioPrendido)
        {
            AudioSource.PlayClipAtPoint(enemyDeadSFX, Camera.main.transform.position);
        }

        var EnemySpawn = FindObjectOfType<EnemySpawner>();
        EnemySpawn.EnemyVivos--;
        EnemySpawn.spawnedEnemies.text =( EnemySpawn.EnemyVivos + 1).ToString();
        Destroy(gameObject);
    }

}
