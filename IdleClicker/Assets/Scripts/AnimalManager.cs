using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager instance;


    public Transform spawnPoint;
    public GameObject[] animalPrefabs;
    public Animal curAnimal;

    private void Awake()
    {
        instance = this;
    }




    public void SpawnAnimal()
    {
        GameObject animalToSpawn = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        GameObject obj = Instantiate(animalToSpawn, spawnPoint);

        curAnimal = obj.GetComponent<Animal>();
    }


    public void ReplaceAnimal(GameObject animal)
    {

        Destroy(animal);
        SpawnAnimal();
    }
}
