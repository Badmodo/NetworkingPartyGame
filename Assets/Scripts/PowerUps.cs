using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    ThirdPersonController controller;

    public GameObject player;

    public Rigidbody playerRigidbody;
     
    public ParticleSystem forcefieldParticle;
    


    /// Find how to change materials temorarily and ignor other player colliders
    //private void Ghost()
    //{
    //    StartCoroutine(ghost());
    //}

    //IEnumerator ghost()
    //{
    //    player.GetComponent<Material>
    //    yield return new WaitForSeconds(4f);
    //    controller.speed = 6f;
    //}

    private void Forcefield()
    {
        StartCoroutine(forcefield());
    }

    IEnumerator forcefield()
    {
        forcefieldParticle.Play();
        //add particle field pulsating and has colliders to puch players
        yield return new WaitForSeconds(4f);
        forcefieldParticle.Stop();
    }

    private void Rocket()
    {
        //find closest enemy and fires a rocket towards their feet
    }


    private void Jetpack()
    {
        //StartCoroutine(jetpack());
    }
    //IEnumerator jetpack()
    //{
    ////    find how to apply a large initial force and maintain for a few second before dropping
    //    playerRigidbody.AddForce(Vector3.up 30f);
    //    yield return new WaitForSeconds(3f);
    //}
}
