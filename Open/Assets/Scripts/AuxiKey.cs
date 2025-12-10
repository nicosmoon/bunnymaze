using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiKey : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && Game.ky == true)
            {
                Destroy(this.gameObject);
                Game.ky = false;

            }
        
    }
}
