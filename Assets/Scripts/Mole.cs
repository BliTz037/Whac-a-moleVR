using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int direction = 1;

    [SerializeField]
    private float distance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When mole's collider is triggered
    private void OnTriggerEnter(Collider other)
    {
        // If the collider is a hammer
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("OK TOUCH !");
            SoundManager.Instance.PlaySound(SoundManager.Instance.hitSound);
            // TODO Play the hit animation
            

            // TODO Play the hit sound

            // Destroy the mole
            //Destroy(gameObject);
        }
    }

    private void Move()
    {

    }
}
