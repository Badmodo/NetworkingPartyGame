using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public GameObject postProcessingJump;

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
        ThirdPersonController.jumpPower = 8f;
        yield return new WaitForSeconds(4f);
        ThirdPersonController.jumpPower = 3f;
        postProcessingJump.SetActive(false);
    }
}
