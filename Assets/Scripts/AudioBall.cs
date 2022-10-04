using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBall : MonoBehaviour
{
    public AudioSource audioBall;
    public PlayerActions player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && player.holdingBall == false)
        {
            audioBall.Play();
        }
    }
}
