using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameObject postProcessingSpeed;

    public float speedBoosted;
    public float duration;
    public float speedOriginal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(speedBoost());
        }
    }

    IEnumerator speedBoost()
    {
        ThirdPersonController.speed = speedBoosted;
        postProcessingSpeed.SetActive(true);
        yield return new WaitForSeconds(duration);
        ThirdPersonController.speed = speedOriginal;
        postProcessingSpeed.SetActive(false);
    }
}
