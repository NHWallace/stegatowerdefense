using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float projectileSpeed = 70f;
    public float explosionRadius = 0f;
    public int damage = 30;
    public GameObject impactEffect;
    [SerializeField] private AudioClip impactSound;

    public void setTarget (Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        // If the target was destroyed, destroy the bullet as well
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        // If bullet has hit the target
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Bullet has not hit target yet, move it towards target
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        SoundFXManager.instance.PlaySoundFXClip(impactSound, transform, SoundFXManager.instance.EffectVolume);
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in collidersHit)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage (Transform enemyObject)
    {
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
