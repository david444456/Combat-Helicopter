using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInicio : MonoBehaviour
{
    [SerializeField] GameObject Tut1;
    [SerializeField] GameObject Tut2;
    
    [SerializeField] GameObject Tut3;
    [SerializeField] GameObject Tut4;
    [SerializeField] JoyButton up;
    bool yapaso = true;


    // Start is called before the first frame update
    void Start()
    {
        Tut1.SetActive(true);
    }
    public void tut1() {
        if (yapaso)
        {
            Tut1.SetActive(false);
            Tut2.SetActive(true);
            Destroy(Tut2, 4f);
            yapaso = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Chocuqeee");
        if (Tut2 != null) { 
             Tut2.SetActive(false); }
        Tut3.SetActive(true);
        var e = GetComponents<BoxCollider>();
        foreach (BoxCollider bx in e) {
            bx.enabled = false;
        }
        Time.timeScale = 0;
    }

    public void tut3() {
        Tut3.SetActive(false);
        Tut4.SetActive(true);
    }
    public void tut4()
    {
        
        Tut4.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (up.Pressed && yapaso) {
            Tut1.SetActive(false);
            Tut2.SetActive(true);
            Destroy(Tut2, 4f);
            yapaso = false;
        }
    }
}
