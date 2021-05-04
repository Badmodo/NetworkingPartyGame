using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platforms;
    
    public float platformSpeed;
    public float platformTimer;
    public GameObject[] platformSpawn = new GameObject[6];
    public GameObject[] platformEnd = new GameObject[6];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < platformSpawn.Length; i++)
        {
            float step = platformSpeed * Time.deltaTime;
            GameObject newPlatform = platforms[Random.Range(0, platforms.Length)];
            Instantiate(newPlatform, platformSpawn[i].transform.position, platformSpawn[i].transform.rotation);
            newPlatform.transform.position = Vector3.MoveTowards(platformSpawn[i].transform.position, platformEnd[i].transform.position, step);
        }
    }
    
}
