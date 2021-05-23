using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platforms.Manager
{
    public class PlatformManager : MonoBehaviour
    {
        public GameObject[] platforms;

        public float platformSpeed;
        public float platformTimer = 2;
        public GameObject[] platformSpawn = new GameObject[6];
        
      

        // Start is called before the first frame update
        void Start()
        {
            platformSpeed *= 0.05f;
            
            StartCoroutine(SpawnPlatform());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartSpawning()
        {

        }

        IEnumerator SpawnPlatform()
        {
            float directionModifier = 180;
            for (int i = 0; i < platformSpawn.Length; i++)
            {


                GameObject newPlatform = platforms[Random.Range(0, platforms.Length)];
                
                GameObject spawnedPlatform = Instantiate(newPlatform, platformSpawn[i].transform.position,
                    platformSpawn[i].transform.rotation );

                spawnedPlatform.transform.rotation *= Quaternion.Euler(0, directionModifier, 0);
                directionModifier += 180;


            }

            yield return new WaitForSeconds(platformTimer * 80);

            StartCoroutine(SpawnPlatform());
        }

    }

}
