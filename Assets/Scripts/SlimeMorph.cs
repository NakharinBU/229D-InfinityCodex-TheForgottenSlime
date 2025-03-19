using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMorph : MonoBehaviour
{
    public enum SlimeState { Solid, Liquid, Gas }
    public SlimeState currentState = SlimeState.Solid;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private Vector3 originalScale;

    public ParticleSystem solidEffect;
    public ParticleSystem liquidEffect;
    public ParticleSystem gasEffect;

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeState(SlimeState.Solid);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeState(SlimeState.Liquid);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeState(SlimeState.Gas);
    }

    void ChangeState(SlimeState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case SlimeState.Solid:
                rb.mass = 2f;
                rb.drag = 1f;
                sphereCollider.enabled = true;
                transform.localScale = originalScale;
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
    {/*
        solidEffect.Stop();
        liquidEffect.Stop();
        gasEffect.Stop();
*/
        if (newState == SlimeState.Solid) solidEffect.Play(true);
        if (newState == SlimeState.Liquid) liquidEffect.Play(true);
        if (newState == SlimeState.Gas) gasEffect.Play(true);
    }

}
