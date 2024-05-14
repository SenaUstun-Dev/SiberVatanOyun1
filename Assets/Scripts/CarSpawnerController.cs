using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CarSpawnerController : MonoBehaviour
{

    public List<GameObject> carPrefabs;
    public float minTime, maxTime;
    
    public float timer;
    public float spawnTime;

    private void Start()
    {
        spawnTime = Random.Range(minTime, maxTime); //burda yazýca baþlangýç deðeri oluyo, ilk araba
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0;

            var car = carPrefabs[Random.Range(0, carPrefabs.Count)];

            var spawnedCar = Instantiate(car,transform.position, transform.rotation, transform);
            spawnedCar.AddComponent<CarController>();
            spawnTime = Random.Range(minTime, maxTime);

        }
        
    }
}
