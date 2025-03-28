using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar;

    public Image solid,liquid,gas;

    private SlimeMorph slimeMorph;

    private bool isPaused = false;
    public PauseMenu pauseMenu;

    private bool isDead = false;
    public PlayAgain player; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        slimeMorph = FindObjectOfType<SlimeMorph>();
        UpdateMorphUI();
    }

    void Update()
    {
        UpdateMorphUI();
        PauseGame();
    }

    void SetColor(Image state) 
    {
        Color newColor = state.color;
        newColor.a = 0.5f;
        state.color = newColor;
    }

    void ResetColor(Image state)
    {
        Color newColor = state.color;
        newColor.a = 1;
        state.color = newColor;
    }

    void UpdateMorphUI()
    {
        switch (slimeMorph.currentState)
        {
            case SlimeMorph.SlimeState.Solid:
                ResetColor(solid);
                SetColor(liquid);
                SetColor(gas);
                break;
            case SlimeMorph.SlimeState.Liquid:
                ResetColor(liquid);
                SetColor(solid);
                SetColor(gas);
                break;
            case SlimeMorph.SlimeState.Gas:
                ResetColor(gas);
                SetColor(liquid);
                SetColor(solid);
                break;
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused && !(currentHealth <= 0))
            {
                pauseMenu.Pause();
            }
            else if (!isPaused)
            {
                pauseMenu.Resume();
                pauseMenu.option.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (isDead == true)
        {
            player.Died();
        }
    }
}
