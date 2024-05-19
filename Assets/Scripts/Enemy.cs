using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float maxHealth = 100f;
    private float health;
    public int moneyDroppedOnDeath = 50;

    public GameObject deathEffect;
    [SerializeField] private AudioClip deathSound;

    public Image healthBar;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, SoundFXManager.instance.EffectVolume);

        PlayerStats.Money += moneyDroppedOnDeath;

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

}
