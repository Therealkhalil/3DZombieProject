using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the attack box trigger with enemy, it will reduce health by attackPower of the players.
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie Attacked!");
        }
    }
}