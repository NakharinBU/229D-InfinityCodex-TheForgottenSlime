using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SlimeMorph : MonoBehaviour
{
    public enum SlimeState { Solid, Liquid, Gas }
    public SlimeState currentState = SlimeState.Solid;
    public SlimeState previousState;

    public PlayerMovement playerMovement;
    public Rigidbody rb;
    public SphereCollider sphereCollider;
    public Vector3 originalScale;

    private AudioSource audioSource;
    public AudioClip solidSFX, liquidSFX, gasSFX;

    public float cooldownTime = 1f;
    private float cooldownTimer = 0.0f;

    public ParticleSystem solidEffect;
    public ParticleSystem liquidEffect;
    public ParticleSystem gasEffect;

    public float solidSpeed = 5f;
    public float liquidSpeed = 8f;
    public float gasSpeed = 3f;

    public float gasVolume = 1.0f;
    public float gasMass = 1.0f;

    public float acceleration = 20f;

    public bool isBeingBlown = false;

    private PhysicMaterial solidMaterial, liquidMaterial, gasMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        originalScale = transform.localScale;
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        solidMaterial = new PhysicMaterial();
        solidMaterial.bounciness = 0.2f;
        solidMaterial.dynamicFriction = 0.5f;
        solidMaterial.staticFriction = 0.6f;

        liquidMaterial = new PhysicMaterial();
        liquidMaterial.bounciness = 0f;
        liquidMaterial.dynamicFriction = 0.2f;
        liquidMaterial.staticFriction = 0.1f;

        gasMaterial = new PhysicMaterial();
        gasMaterial.bounciness = 0f;
        gasMaterial.dynamicFriction = 0.0f;
        gasMaterial.staticFriction = 0.0f;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && cooldownTimer <= 0)
            ChangeState(SlimeState.Solid);
        if (Input.GetKeyDown(KeyCode.Alpha2) && cooldownTimer <= 0)
            ChangeState(SlimeState.Liquid);
        if (Input.GetKeyDown(KeyCode.Alpha3) && cooldownTimer <= 0)
            ChangeState(SlimeState.Gas);
    }

    void ChangeState(SlimeState newState)
    {
        if (cooldownTimer > 0) return;
        if (currentState == newState) return;

        cooldownTimer = cooldownTime;
        previousState = currentState;
        currentState = newState;

        playerMovement.UpdateMorphState(newState);

        switch (newState)
        {
            case SlimeState.Solid:
                rb.mass = 5f;
                rb.drag = 1f;
                rb.useGravity = true;
                sphereCollider.material = solidMaterial;
                sphereCollider.enabled = true;
                transform.localScale = originalScale;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableForLiquid"), false);
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableWall"), false);
                audioSource.PlayOneShot(solidSFX);
                break;

            case SlimeState.Liquid:
                rb.mass = 1.5f;
                rb.drag = 3f;
                rb.useGravity = true;
                sphereCollider.material = liquidMaterial;
                transform.localScale = originalScale * 0.7f;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableForLiquid"), true);
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableWall"), false);
                audioSource.PlayOneShot(liquidSFX);
                break;

            case SlimeState.Gas:
                rb.mass = 0.2f;
                rb.drag = 0.1f;
                rb.useGravity = false;
                sphereCollider.material = gasMaterial;
                transform.localScale = originalScale * 1.3f;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableWall"), true);
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableForLiquid"), true);
                audioSource.PlayOneShot(gasSFX);
                break;
        }

        ChangeStateEffect(newState);
    }

    void ChangeStateEffect(SlimeState newState)
    {
        if (solidEffect.isPlaying) solidEffect.Stop();
        if (liquidEffect.isPlaying) liquidEffect.Stop();
        if (gasEffect.isPlaying) gasEffect.Stop();

        switch (newState)
        {
            case SlimeState.Solid:
                solidEffect.Play();
                break;
            case SlimeState.Liquid:
                liquidEffect.Play();
                break;
            case SlimeState.Gas:
                gasEffect.Play();
                break;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (currentState == SlimeState.Liquid)
        {
            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, collision.contacts[0].normal);
            rb.AddForce(slopeDirection * liquidSpeed, ForceMode.Acceleration);
        }
    }
}
