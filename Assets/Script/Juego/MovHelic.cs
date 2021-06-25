using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovHelic : MonoBehaviour
{
    [Tooltip("M/s")] [SerializeField] float xSpeed = 4f;
    [Tooltip("M")] [SerializeField] float Range = 10f;
    [Tooltip("M")] [SerializeField] float yRange = 5f;
    [SerializeField] float rotacionHelic;
    [SerializeField] float ValorRotacion;
    float yThrow;
    float xThrow;

    [SerializeField] float YRotation;
    [SerializeField] float zRotation;
    [SerializeField] float Avances;
    bool OnEnableController = true;
    bool IsMoving = false;


    [SerializeField] float speed = 10f;
    [SerializeField] float upSpeed = 2f;
    [SerializeField] float maxSpeed = 10f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        /*Quaternion deviceRotation = DeviceRotation.Get();  
        transform.rotation = deviceRotation;
        //rb.AddForce();
        rb.AddForce(transform.up * upSpeed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        */


        //el otro
        if (OnEnableController)
        {/*
            Traslation();
            MovimientoFrontal();
            ProcessRotation();
            */
        }
    }

    private void MovimientoFrontal()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.localPosition = new Vector3(Avances + transform.localPosition.x, transform.localPosition.y,transform.localPosition.z);
            IsMoving = true;
            if (ValorRotacion >= -20)
            {
                transform.Rotate(0, 0, rotacionHelic);
                ValorRotacion = ValorRotacion + 0.5f;
            }
            
        }
        else
        {
            if (ValorRotacion >= 0) return;
            ValorRotacion = ValorRotacion-rotacionHelic;
            transform.Rotate(0, 0, -rotacionHelic);
        }
        IsMoving = false;

        
    }

    private void ProcessRotation()
    {
        if (!(xThrow==0) || !(yThrow==0)) {
            float pitch = YRotation * xThrow * Time.deltaTime;
            float yaw = zRotation * xThrow * 0.1f;
            float roll = transform.localPosition.z;
            transform.Rotate( 0, yaw, pitch);
        }
    }

    private void Traslation()
    {
        xThrow = Input.GetAxis("Horizontal");


        yThrow = Input.GetAxis("Vertical");


        transform.localPosition = new Vector3(transform.localPosition.x, yThrow + transform.localPosition.y, -xThrow + transform.localPosition.z);
    }
}
