using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    public float damage = 10f;
    public float activeTime = 1f;
    public float cooldownTime = 2f;

    public ParticleSystem flameEffect;
    private Collider flameCollider;
    private bool isActive = false;

    void Start()
    {
        flameCollider = GetComponent<Collider>();
        if (flameCollider != null)
        {
            flameCollider.isTrigger = true;
            flameCollider.enabled = false;
        }

        StartCoroutine(FlameLoop());
    }

    private IEnumerator FlameLoop()
    {
        while (true)
        {
            ActivateFlame();
            yield return new WaitForSeconds(activeTime);
            DeactivateFlame();
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    void ActivateFlame()
    {
        isActive = true;
        if (flameEffect != null) flameEffect.Play();
        if (flameCollider != null) flameCollider.enabled = true;
    }

    void DeactivateFlame()
    {
        isActive = false;
        if (flameEffect != null) flameEffect.Stop();
        if (flameCollider != null) flameCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            UIManager playerHealth = other.GetComponent<UIManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
