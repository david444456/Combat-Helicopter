using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float Range = 50f;

    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float timeToRevive=5f;
    bool necesidadPorDead = true;

    public Waypoint baseWaypoint;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (objectToPan == null) return;
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            //objectToPan.rotation = Quaternion.Euler(0 + objectToPan.transform.rotation.x, 90+objectToPan.transform.rotation.y, 0+ objectToPan.transform.rotation.z);
            objectToPan.transform.Rotate(0, 90, 0);
            FireAtEnemy();
        }else
        {
            Shoot(false);
        }
    }

    public void setPartycle(float velParticula, float emisionParticula) {
        var part = projectileParticles.emission;
        part.rateOverTime = emisionParticula;
        var part2 = projectileParticles.main;
        part2.startSpeed = velParticula;
        
       // projectileParticles.emission.rateOverTime = 0f;

    }

    private void SetTargetEnemy() //vamos a verificar quien esta mas cerca dse las torres
    {
        if (targetEnemy == null) {
            HelicopterController game = FindObjectOfType<HelicopterController>();
            targetEnemy = game.transform;
        }
        //var sceneEnemis = FindObjectsOfType<EnemyDamage>();
        //if (sceneEnemis.Length == 0) { return; }
       // Transform closesEnemy = sceneEnemis[0].transform;

        //foreach (EnemyDamage testEnemy in sceneEnemis) {
           //Transform closesEnemy = GetClosest(closesEnemy, testEnemy.transform);
        //}
        //targetEnemy = closesEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToA < distToB)
        {
            return transformA;
        }
        else {
            return transformB;
        }
        
    }

    void FireAtEnemy() {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= Range)
        {
            Shoot(necesidadPorDead);
        }
        else {
            Shoot(false);
        }
    }

    public void Shoot(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

    public void Dead() {
        //mover alguna parte o desactivar algo para parecer que me re mori

      

        necesidadPorDead = false;
        print("Apague una torre " + gameObject.name);
        if (objectToPan != null) objectToPan.gameObject.SetActive(false);

        Invoke("Respawn", timeToRevive);

        
    }

    void Respawn() {
        if (objectToPan != null) objectToPan.gameObject.SetActive(true);
        necesidadPorDead = true;
        print("encendi una torre " + gameObject.name);
    }
    /*
    private void OnDrawGizmos()
    {
        Color c = new Color(1, 0, 0, 0.75f);
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, Range);
    }*/
    
}
