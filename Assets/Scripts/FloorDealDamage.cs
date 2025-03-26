using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDealDamage : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager playerHealth = other.GetComponent<UIManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage * Time.deltaTime);
            }
        }
    }
}
