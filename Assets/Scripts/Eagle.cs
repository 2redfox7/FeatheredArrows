using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Eagle : MonoBehaviour
{
    public GameObject[] eagleSpawners;
    private GameObject[] otherSpawners;
    public GameObject eagle;
    private GameObject currentSpawner;
    private GameObject currentEagle;
    private int spawnerIndex;

    //public Transform Player;

    private bool spawnerActivation;

    public float flyingSpeed;
    private bool isFlying;
    private void Start()
    {
        currentSpawner = eagleSpawners[spawnerIndex];
        targetIndexGenerator(eagleSpawners);
        currentSpawner.SetActive(true);
        spawnerActivation = true;
        isFlying = false;
    }
    private void Update()
    {
        //GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        if (!spawnerActivation || !currentSpawner.activeInHierarchy)
        {
            targetIndexGenerator(otherSpawners);
            spawnerActivation = true;
        }

        currentSpawner = eagleSpawners[spawnerIndex];
        otherSpawners = RemoveAt(eagleSpawners, spawnerIndex);
        if (!isFlying) {
            currentSpawner.SetActive(true);
            currentEagle = Instantiate(eagle, currentSpawner.transform.position, currentSpawner.transform.rotation); 
        }
        if (currentSpawner)
        {
            isFlying = true;
            eagleMovement(currentEagle);
        }
        foreach (GameObject disactivatedSpawner in otherSpawners)
        {
            disactivatedSpawner.SetActive(false);
        }
    }
    private int targetIndexGenerator(GameObject[] eagleSpawners)
    {
        spawnerIndex = Random.Range(0, eagleSpawners.Length);
        return spawnerIndex;
    }
    private GameObject[] RemoveAt(GameObject[] array, int index)
    {
        GameObject[] newArray = new GameObject[array.Length - 1];

        for (int i = 0; i < index; i++)
            newArray[i] = array[i];

        for (int i = index + 1; i < array.Length; i++)
            newArray[i - 1] = array[i];
        array = newArray;
        return array;
    }
    private void eagleMovement(GameObject eagle)
    {
            eagle.transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
    }
}
