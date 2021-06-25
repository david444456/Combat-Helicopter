using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAds : MonoBehaviour
{

    [SerializeField] ParticleSystem vfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var V = Instantiate(vfx.gameObject, transform.position + new Vector3(-1,0,0), Quaternion.identity);
        var V2 = Instantiate(vfx.gameObject, transform.position + new Vector3(-1, -1, 0), Quaternion.identity);
        var V3 = Instantiate(vfx.gameObject, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        V.GetComponent<ParticleSystem>().Play();
        V2.GetComponent<ParticleSystem>().Play();
        V3.GetComponent<ParticleSystem>().Play();
    }
}
