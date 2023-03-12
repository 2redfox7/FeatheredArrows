using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class chickenAnimation : MonoBehaviour
{
    public Animator animator;
    public float Speed = 1f;
    private bool walk;
    private bool turn_head;
    private float time;
    void Start()
    {
        animator = GetComponent<Animator>();
        walk = true;
    }
    private void Update()
    {
        animator.SetBool("Walk", walk);
        animator.SetBool("Turn Head", turn_head);
    }
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time <= 3)
        {
            walk = true;
            MovementLogic();
        }
        if (time > 3)
        {
            walk = false;
        }
        if (time > 10)
        {
            turn_head = true;
        }
        if (time > 12)
        {
            turn_head = false;
            time = 5;
        }
    }

    private void MovementLogic()
    {
        if (walk)
        {
            transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime);
        }
    }
}
