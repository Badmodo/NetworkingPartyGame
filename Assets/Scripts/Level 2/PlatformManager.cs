using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platforms.Manager
{
    public class PlatformManager : MonoBehaviour
    {
        public GameObject[] platforms;

        public float platformSpeed;
        public float platformTimer;
        public GameObject[] platformSpawn = new GameObject[6];
        public GameObject[] platformEnd = new GameObject[6];
        public Transform end;

        // Start is called before the first frame update
        void Start()
        {
            platformSpeed = platformSpeed * Time.deltaTime;
            StartSpawning();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void StartSpawning()
        {
            for (int i = 0; i < platformSpawn.Length; i++)
            {


                GameObject newPlatform = platforms[Random.Range(0, platforms.Length)];
                // Spawn platform on the 
                end = platformEnd[i].transform;
                Instantiate(newPlatform, platformSpawn[i].transform.position, platformSpawn[i].transform.rotation);
                // Move platform towards end

                
            }
        }
       




    }

}
