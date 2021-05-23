using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpawning : MonoBehaviour
{
    [SerializeField] private GameObject lava;

    private Vector3 newSpawn = new Vector3(0, 0, 75);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(lava, transform.position + newSpawn , transform.rotation);
            Destroy(gameObject);
        }

            
    }
}
