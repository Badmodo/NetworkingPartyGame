using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameObject postProcessingSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(speedBoost());
        }
    }

    IEnumerator speedBoost()
    {
        ThirdPersonController.speed = 12f;
        postProcessingSpeed.SetActive(true);
        yield return new WaitForSeconds(4f);
        ThirdPersonController.speed = 6f;
        postProcessingSpeed.SetActive(false);
    }
}
