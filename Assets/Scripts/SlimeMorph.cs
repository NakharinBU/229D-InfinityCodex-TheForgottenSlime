﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SlimeMorph : MonoBehaviour
{
    public enum SlimeState { Solid, Liquid, Gas }
    public SlimeState currentState = SlimeState.Solid;
    public SlimeState previousState;

    private PlayerMovement playerMovement;

    AudioSource audioSource;
    public AudioClip solidSFX, liquidSFX, gasSFX;

    public float cooldownTime = 1.5f;
    public float cooldownTimer = 0.0f;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private Vector3 originalScale;

    public ParticleSystem solidEffect;
    public ParticleSystem liquidEffect;
    public ParticleSystem gasEffect;

    private bool hasPlayedSolidEffect = false;
    private bool hasPlayedLiquidEffect = false;
    private bool hasPlayedGasEffect = false;

    public float solidSpeed = 5f;
    public float liquidSpeed = 8f;
    public float gasSpeed = 3f;

    public float solidJump = 7f;
    public float gasFloatSpeed = 4f;

    public bool isBeingBlown = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        originalScale = transform.localScale;
        playerMovement = GetComponent<PlayerMovement>();
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

        if (currentState == SlimeState.Gas && !playerMovement.isTouchingCeiling)
        {
            rb.AddForce(Vector3.up * gasFloatSpeed, ForceMode.Acceleration);
        }
    }
    void FixedUpdate()
    {
        if (currentState == SlimeState.Gas && isBeingBlown)
        {
            rb.AddTorque(Vector3.up * 5f, ForceMode.Acceleration);
        }
    }


    void ChangeState(SlimeState newState)
    {
        if (cooldownTimer > 0) return;
        cooldownTimer = cooldownTime;

        if (currentState == newState) return;
        previousState = currentState;
        currentState = newState;

        playerMovement.UpdateMorphState(newState);


        switch (newState)
        {
            case SlimeState.Solid:
                //gameObject.tag = "SolidSlime";
                rb.mass = 5f;
                rb.drag = 1f;
                sphereCollider.enabled = true;
                transform.localScale = originalScale;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableForLiquid"), false);
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableWall"), false);
                audioSource.PlayOneShot(solidSFX);
                break;

            case SlimeState.Liquid:
                //gameObject.tag = "LiquidSlime";
                rb.mass = 1f;
                rb.drag = 3f;
                transform.localScale = originalScale * 0.7f;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableForLiquid"), true);
                audioSource.PlayOneShot(liquidSFX);
                break;

            case SlimeState.Gas:
                //gameObject.tag = "GasSlime";
                rb.mass = 0.5f;
                rb.drag = 0.2f;
                transform.localScale = originalScale * 1.3f;
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("PassableWall"), true);
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

        if (newState == SlimeState.Solid && !hasPlayedSolidEffect)
        {
            solidEffect.Play();
            hasPlayedSolidEffect = true;
            hasPlayedLiquidEffect = false;
            hasPlayedGasEffect = false;
        }
        else if (newState == SlimeState.Liquid && !hasPlayedLiquidEffect)
        {
            liquidEffect.Play();
            hasPlayedLiquidEffect = true;
            hasPlayedSolidEffect = false;
            hasPlayedGasEffect = false;
        }
        else if (newState == SlimeState.Gas && !hasPlayedGasEffect)
        {
            gasEffect.Play();
            hasPlayedGasEffect = true;
            hasPlayedSolidEffect = false;
            hasPlayedLiquidEffect = false;
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
