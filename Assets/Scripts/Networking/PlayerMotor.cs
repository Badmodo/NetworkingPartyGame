using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float speed = 6;

    private bool isSetup = false;

    public void Setup()
    {
        isSetup = true;
    }


    void Update()
    {
        if (!isSetup)
            return;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    }
}
