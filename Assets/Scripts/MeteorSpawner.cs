using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    private float spawnNext = 0;
    public float spawnRatePerMinute = 30;
    public float spawnRateIncrement = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnNext) {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            var rand = Random.Range(-9, 9);
            var spawnVector = new Vector2(rand, 7);
            Instantiate(asteroidPrefab, spawnVector, Quaternion.identity);
        }
    }
}
