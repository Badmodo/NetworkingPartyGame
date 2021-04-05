using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public GameObject postProcessingJump;

    public float jumpBoosted;
    public float duration;
    public float jumpOriginal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(superJump());
        }
    }

    IEnumerator superJump()
    {
        postProcessingJump.SetActive(true);
        ThirdPersonController.jumpPower = jumpBoosted;
        yield return new WaitForSeconds(duration);
        ThirdPersonController.jumpPower = jumpOriginal;
        postProcessingJump.SetActive(false);
    }
}
