using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChomper : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Killing  Player");
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("RespawnPlayer");
        }
    }
}
