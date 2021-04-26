using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    public GameObject forcefieldParticle;

    public float duration;

    // OnParticleCollision is called when a particle hits a collider
    private void OnParticleCollision(GameObject other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(forcefield());
        }
    }

    IEnumerator forcefield()
    {
        forcefieldParticle.SetActive(true);
        //add particle field pulsating and has colliders to puch players
        yield return new WaitForSeconds(duration);
        forcefieldParticle.SetActive(false);
    }
}
