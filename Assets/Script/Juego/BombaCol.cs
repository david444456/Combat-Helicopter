using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombaCol : MonoBehaviour
{
    public int valorHitElement;

    [SerializeField] bool isMisil;
    [SerializeField] ParticleSystem BombaDead;
    [SerializeField] ParticleSystem TorresDead;
    [SerializeField] AudioClip bomb;
    [SerializeField] AudioSource audioBomb;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        audioBomb =  FindObjectOfType<ControlPanel>().GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMisil) {
            rb.AddForce(transform.forward *5);
            //transform.localPosition = transform.localPosition + new Vector3(1* transform.rotation.x, 1 * transform.rotation.y, 1 * transform.rotation.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ExplosionEstilo();
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
            return; }
        

        if (collision.gameObject.tag == "Torres")
        {
            var vfx2 = Instantiate(TorresDead, collision.gameObject.GetComponent<Tower>().objectToPan.transform.position, Quaternion.identity);
            vfx2.Play();
            float destroyDelay2 = vfx2.main.duration;
            Destroy(vfx2.gameObject, destroyDelay2);
            collision.gameObject.GetComponent<Tower>().Dead();
            
            //Destroy(collision.gameObject);

        }
        Destroy(gameObject);
    }

   public void ExplosionEstilo() {
        var vfx = Instantiate(BombaDead, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);
        audioBomb.clip = bomb;
        audioBomb.Play();
    }
}
