using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Material[] newMaterial; // 0 = Solid, 1 = Liquid, 2 = Gas
    public int materialIndex = 1;
    public SlimeMorph slimeMorph;

    private SkinnedMeshRenderer skinnedRenderer;

    void Start()
    {
        skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (slimeMorph == null)
        {
            slimeMorph = FindObjectOfType<SlimeMorph>();
        }

        if (skinnedRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer not found on this GameObject!");
        }
    }

    private void Update()
    {
        if (skinnedRenderer != null && slimeMorph != null)
        {
            Material[] mats = skinnedRenderer.materials;

            switch (slimeMorph.currentState)
            {
                case SlimeMorph.SlimeState.Solid:
                    mats[materialIndex] = newMaterial[0];
                    break;
                case SlimeMorph.SlimeState.Liquid:
                    mats[materialIndex] = newMaterial[1];
                    break;
                case SlimeMorph.SlimeState.Gas:
                    mats[materialIndex] = newMaterial[2];
                    break;
            }

            skinnedRenderer.materials = mats; // Update new Material
        }
        else
        {
            Debug.LogWarning("Missing SkinnedMeshRenderer or SlimeMorph!");
        }
    }
}
