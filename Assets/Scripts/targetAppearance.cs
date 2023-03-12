using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;
public class targetAppearance : MonoBehaviour
{
    public GameObject[] targetsPool;
    private GameObject[] otherTargets;
    private GameObject currentTarget;

    private int targetIndex = 0;
    private int currentIndex = 0;
    public static bool targetActivation;

    private void Start()
    {
        otherTargets = targetsPool;
        currentTarget = targetsPool[targetIndex];
        targetIndexGenerator(targetsPool);
        currentTarget.SetActive(true);
        targetActivation = true;
        currentIndex = targetIndex;
    }

    void Update()
    {
        if (!targetActivation || !currentTarget.activeInHierarchy)
        {
            while (targetIndex == currentIndex) 
            {
                targetIndexGenerator(otherTargets);
                targetActivation = true;
            }
            currentIndex = targetIndex;
        }

        currentTarget = targetsPool[targetIndex];
        otherTargets = RemoveAt(targetsPool, targetIndex);

        if (currentTarget)
        {
            currentTarget.SetActive(true);
        }

        foreach (GameObject disactivatedTarget in otherTargets)
        {
            disactivatedTarget.SetActive(false);
        }

    }
    private int targetIndexGenerator(GameObject[] targetsPool)
    {
        targetIndex = Random.Range(0, targetsPool.Length);
        return targetIndex;
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
}