using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;
using Mirror;
public class targetAppearanceMP : NetworkBehaviour
{
    public GameObject[] targetsPool;
    private GameObject[] otherTargets;
    private GameObject currentTarget;

    private int targetIndex = 0;
    private int currentIndex = 0;
    
    public static bool targetActivation;

    private bool outlineActivation = true;

    private void Awake()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 4f;
    }
    private void Start()
    {
        otherTargets = targetsPool;
        currentTarget = targetsPool[targetIndex];
        targetIndexGenerator(otherTargets);
        currentTarget.SetActive(true);
        targetActivation = true;
        currentIndex = targetIndex;
    }

    void Update()
    {
        if (PauseMP.GameIsPaused || TimerMP.GameIsEnd)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 0f;
            outlineActivation = false;
        }
        else if (!outlineActivation && !PauseMP.GameIsPaused && !TimerMP.GameIsStart)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 4f;
            outlineActivation = true;
        }
        if (!targetActivation || !currentTarget.activeInHierarchy)
        {

            while (targetIndex == currentIndex)
            {

                    targetIndexGenerator(otherTargets);   

                    //CmdTargetIndexGenerator(otherTargets);

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
    //[Server]
    public int targetIndexGenerator(GameObject[] targetsPool)
    {
        targetIndex = Random.Range(0, targetsPool.Length);
        return targetIndex;
    }
    //[Command] //обозначаем, что этот метод должен будет выполняться на сервере по запросу клиента
    //public void CmdTargetIndexGenerator(GameObject[] targetsPool)
    //{
    //    targetIndexGenerator(targetsPool);
    //}
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