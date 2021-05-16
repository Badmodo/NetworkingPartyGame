using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operators : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    private void Start()
    {
        //if the object is null, run the right, otherwise return the value
        rigidbody ??= gameObject.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            return;
        }
        else
        {

        }
    }

    private void Update()
    {
        //if the object is null ignor the function
        rigidbody?.AddForce(Vector3.up * 100f);
    }
}
