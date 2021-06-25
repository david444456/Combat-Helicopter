using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirLuz : MonoBehaviour
{
    [SerializeField] Vector3 posError;
    [SerializeField]
    GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) {
            HelicopterController game = FindObjectOfType<HelicopterController>();
            Target = game.gameObject;
        }
        transform.position = Target.transform.position + posError;
        
    }
}
