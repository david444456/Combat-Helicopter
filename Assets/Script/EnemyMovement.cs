using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] Vector3 correccion;
    [SerializeField] ParticleSystem goalParticle;
    [SerializeField] float movementDistance = 1;
    [SerializeField] EnemySpawner ENSpawner;
    PathFinder pathfinder;
    Waypoint waipo;

    public GameObject body;
    public bool llegueAlFinal = false;
    bool meAgregueUnaVez = false;

    Quaternion startROT;

    bool vengodeX;
    bool vengodeZ;
    bool vengodemX;
    bool vengodemZ;
    // Start is called before the first frame update
    private void Awake()
    {
        ENSpawner = FindObjectOfType<EnemySpawner>();
        //para controlar el enemigo desde otro script
        movementPeriod = ENSpawner.movementEnemy;
    }

    void Start()

    {
        

        //cosas basicas

        startROT = transform.rotation;
        


        pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }
    private void Update()
    {
       
    }
    public void StartCou() {
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

   public IEnumerator FollowPath(List<Waypoint> path)
    {
        transform.rotation = Quaternion.FromToRotation(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), new Vector3(0, 0, 0));
        foreach (Waypoint waypoint in path) {
            
            //transform.position = Vector3.Lerp(transform.position, waypoint.transform.position + correccion, movementPeriod);
            var currentPos = transform.position;

            

            float t = 0;
            while (t < 1) {
                t += Time.deltaTime / movementPeriod;
                transform.position = Vector3.Lerp(currentPos, waypoint.transform.position, t);
                analizarRotacion(waypoint.transform.position, currentPos);
                yield return null;
            }
            //yield return new WaitForSeconds(movementPeriod);
            waipo = waypoint;
        }
        //if (!meAgregueUnaVez)

        vengodeX = false;
        vengodeZ = false;
        vengodemZ = false;
        vengodemX = false;
        ENSpawner.agregarLista(gameObject);
        meAgregueUnaVez = true;
        
        SelfDestruct();
    }
    void analizarRotacion(Vector3 waypoint, Vector3 currentPos) {
        if (waypoint.x > currentPos.x)
        {
            if (vengodeZ)
            {

                transform.Rotate(0, 90, 0);
            }
            if (vengodemZ) {
                transform.Rotate(0, -90, 0);
            }
            
            vengodemZ = false;
            vengodemX = false;
            vengodeZ = false;
            vengodeX = true;
        }
        if (waypoint.z > currentPos.z)
        {
            if (vengodeX)
            {
                transform.Rotate(0, -90, 0);
            }
            if (vengodemX)
            {
                transform.Rotate(0, 90, 0);
            }
            
            vengodemZ = false;
            
            vengodeX = false;
            vengodemX = false;
            vengodeZ = true;
        }
        if (waypoint.x < currentPos.x)
        {
            if (vengodemZ)
            {

                transform.Rotate(0, 90, 0);
                
            }
            if (vengodeZ)
            {

                transform.Rotate(0, -90, 0);

            }
            

            vengodemZ = false;
            vengodeX = false;
            vengodeZ = false;
            vengodemX = true;
        }
        if (waypoint.z < currentPos.z)
        {
            if (vengodeX )
            {
                transform.Rotate(0, 90, 0);
            }if (vengodemX)
            {
                transform.Rotate(0, 270, 0);
            }

                
            
            vengodeX = false;
            vengodeZ = false;
            vengodemX = false;
            vengodemZ = true;
        }
    }

    void SelfDestruct()
    {
        print("SelfDestruct");
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);
        if (gameObject.tag == "Patrulla") {
            body = gameObject.GetComponentInChildren<Text>().gameObject;
        }
        else
        {
            body = gameObject.GetComponentInChildren<BoxCollider>().gameObject;
        }
        body.SetActive(false);
        llegueAlFinal = true;
        //Destroy(gameObject);
    }
}
