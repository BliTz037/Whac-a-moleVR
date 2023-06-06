using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // When mole's collider is triggered
    void OnTriggerEnter(Collider other)
    {
        //If the collider is a hammer
        if (other.gameObject.CompareTag("Mole"))
        {
            GameController.instance.KillMole(other.gameObject);
        }
    }
}
