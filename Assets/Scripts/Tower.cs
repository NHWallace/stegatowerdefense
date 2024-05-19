using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f; // Rate is in shots fired per second
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private AudioClip shootSound;


    private void Start()
    {
        // Form of infinitely repeating co-routine, repeats every x seconds
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Find nearest enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // If nearest enemy is in range, target it. Else, target nothing
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        // Do nothing if Tower has no target
        if (target == null)
        {
            return;
        }

        // Lock onto target
        Vector3 direction = target.position - transform.position;
        Quaternion quatLookRotation = Quaternion.LookRotation(direction);
        // Lerp allows a rotation from angle x to y to happen smoothly over time rather happen instantly
        Vector3 lookRotation = Quaternion.Lerp(partToRotate.rotation, quatLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);

        // Fire if not on cooldown
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(shootSound, transform, SoundFXManager.instance.EffectVolume);
            bullet.setTarget(target);
        }
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}