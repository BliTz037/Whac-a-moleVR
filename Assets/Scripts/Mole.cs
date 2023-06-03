using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{

    // When mole's collider is triggered
    private void OnTriggerEnter(Collider other)
    {
        // If the collider is a hammer
        if (other.gameObject.CompareTag("Weapon"))
        {
            GameController.instance.KillMole(this.gameObject);
        }
    }
}
