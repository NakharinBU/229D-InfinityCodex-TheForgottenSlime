using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMorph : MonoBehaviour
{
    public enum SlimeState { Solid, Liquid, Gas }
    public SlimeState currentState = SlimeState.Solid;

    private bool isSolid = false;
    private bool isLiquid = false;
    private bool isGas = false;

    public float cooldownTime = 2.0f;
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
    public float gasFloatSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        originalScale = transform.localScale;
        
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

        if (currentState == SlimeState.Gas)
        {
            rb.AddForce(Vector3.up * gasFloatSpeed, ForceMode.Acceleration);
        }
    }

    void ChangeState(SlimeState newState)
    {
        if (cooldownTimer > 0) return;
        
        cooldownTimer = cooldownTime;
        
        currentState = newState;

        switch (newState)
        {
            case SlimeState.Solid:
                rb.mass = 2f;
                rb.drag = 1f;
                sphereCollider.enabled = true;
                transform.localScale = originalScale;
                isSolid = true;
                break;

            case SlimeState.Liquid:
                rb.mass = 1f;
                rb.drag = 3f;
                sphereCollider.enabled = false;
                transform.localScale = originalScale * 0.7f;
                break;

            case SlimeState.Gas:
                rb.mass = 0.5f;
                rb.drag = 0.2f;
                sphereCollider.enabled = false;
                transform.localScale = originalScale * 1.3f;
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

}
