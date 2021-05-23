using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platforms
{
    public class PlatformTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject platformHub;
       
        private Vector3 newSpawn = new Vector3(0, 0, 36);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Instantiate(platformHub, transform.position + newSpawn , transform.rotation);
                Destroy(gameObject);
            }

            
        }
    }
}
