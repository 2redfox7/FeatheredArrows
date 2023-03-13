using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowRope : MonoBehaviour
{
    public float Tension;
    private bool _pressed;
    public static bool fly = false;

    public Transform RopeTransform;

    public Vector3 RopeNearLocalPosition;
    public Vector3 RopeFarLocalPosition;

    public AnimationCurve RopeReturnAnimation;

    public float ReturnTime;
    public float arrowSpeed;

    public Arrow CurrentArrow;

    public AudioSource BowTension;
    public AudioSource ArrowWhistling;


    void Start()
    {
        RopeNearLocalPosition = RopeTransform.localPosition;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressed = true;

            
            CurrentArrow.SetToRope(RopeTransform);
            BowTension.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            BowTension.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            fly = true;
            _pressed = false;
            StartCoroutine(RopeReturn());
            CurrentArrow.Shot(arrowSpeed * Tension);


            Tension = 0;

            BowTension.Stop();

            ArrowWhistling.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            ArrowWhistling.Play();
        }
        if (_pressed)
        {
            if (Tension < 1f)
            {
                Tension += Time.deltaTime;
            }
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);

        }
    }

    IEnumerator RopeReturn()
    {
        Vector3 startLocalPosition = RopeTransform.localPosition;
        for (float f = 0; f < 1f; f += Time.deltaTime / ReturnTime)
        {
            RopeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RopeNearLocalPosition, RopeReturnAnimation.Evaluate(f));
            yield return null;
        }
        RopeTransform.localPosition = RopeNearLocalPosition;

    }
}
