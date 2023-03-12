using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenPeck : MonoBehaviour
{
    public Animator animator;
    private bool eat;
    private float time;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("Eat", eat);
    }
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= 15)
        {
            eat = true;
        }
        if (time >= 18)
        {
            eat = false;
            time = 0;
        }
    }
}
