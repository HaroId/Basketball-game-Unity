using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruggerMetal : MonoBehaviour
{
    public AudioSource sonido;
    public PlayerActions player;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball" && player.holdingBall == false)
        {
            sonido.Play();
        }
    }
}
