using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] GameObject helic;
    [SerializeField] float distanceHelicY;
    [SerializeField] float distanceHelicX;
    [SerializeField] float distanceHelicZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3( helic.transform.position.x + distanceHelicX, helic.transform.position.y + distanceHelicY, helic.transform.position.z + distanceHelicZ);

    }
}
