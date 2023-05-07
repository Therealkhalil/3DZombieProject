using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        

        //if the attack box trigger with enemy, it will reduce health by attackPower of the players.
        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie Attacked!");
            Destroy(other.gameObject);
        }
        else
            Debug.Log("player");
    }

}