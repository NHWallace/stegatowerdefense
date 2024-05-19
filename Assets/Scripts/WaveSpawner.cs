using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 2.5f;

    [SerializeField] TMP_Text waveCountdownLabel;

    private int waveNumber = 0;
    public GameObject GameWonUI;

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            waveCountdownLabel.text = $"Wave {waveNumber+1}! {EnemiesAlive} enemies left!";
            return;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        // Prevent countdown from becoming negative in the case of a bug
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownLabel.text = "Time until next wave: " + string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        while (EnemiesAlive > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        waveNumber++;
        PlayerStats.RoundsSurvived++;

        if (waveNumber == waves.Length)
        {
            GameWonUI.SetActive(true);
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
