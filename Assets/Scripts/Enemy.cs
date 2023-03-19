using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public GameObject[] eagleSpawners;
    private GameObject[] otherSpawners;
    public GameObject eagle;
    private GameObject currentSpawner;
    private GameObject currentEagle;
    private int spawnerIndex = 0;
    private int currentIndex = 0;

    private bool outlineActivation = true;
    float time = 0;
    private bool spawnerActivation;
    public float flyingSpeed;
    private bool isFlying;

    private void Awake()
    {
        if (eagle.GetComponent<Outline>() == null)
        {
            eagle.AddComponent<Outline>();
        }
        var outline = eagle.GetComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
    }
    private void Start()
    {
        otherSpawners = eagleSpawners;
        targetIndexGenerator(eagleSpawners);
        currentSpawner = eagleSpawners[spawnerIndex];
        currentSpawner.SetActive(true);
        spawnerActivation = true;
        isFlying = false;
        currentIndex = spawnerIndex;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (Pause.GameIsPaused && currentEagle)
        {
            currentEagle.GetComponent<Outline>().OutlineWidth = 0f;
            outlineActivation = false;
        }
        else if (!outlineActivation && !Pause.GameIsPaused && !Timer.GameIsStart && currentEagle)
        {
            currentEagle.GetComponent<Outline>().OutlineWidth = 5f;
            outlineActivation = true;
        }
        if (!Timer.GameIsStart && Mathf.Round(time) >= 10)
        {
            
            if (!spawnerActivation || !currentSpawner.activeInHierarchy)
            {
                while (spawnerIndex == currentIndex)
                {
                    targetIndexGenerator(eagleSpawners);
                }
                spawnerActivation = true;
                currentIndex = spawnerIndex;
            }
            currentSpawner = eagleSpawners[spawnerIndex];
            otherSpawners = RemoveAt(eagleSpawners, spawnerIndex);
            if (!isFlying)
            {
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
            if (Mathf.Round(time) % 12 == 0 && Mathf.Round(time) >= 13)
            {
                spawnerActivation = false;
                currentSpawner.SetActive(false);

                Destroy(currentEagle.gameObject);
                
                isFlying = false;
            }
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
