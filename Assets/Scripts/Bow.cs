using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float Tension;
    private bool _pressed;

    public Transform RopeTransform;

    public Vector3 RopeNearLocalPosition;
    public Vector3 RopeFarLocalPosition;

    public AnimationCurve RopeReturnAnimation;

    public float ReturnTime;


    private void Start()
    {
        RopeNearLocalPosition = RopeTransform.localPosition;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            _pressed = true;
        }
        if(Input.GetMouseButtonUp(0)){
            _pressed = false;
            Tension = 0;
            StartCoroutine(RopeReturn());
        }
        if (_pressed) { 
            if(Tension < 1f)
            {
                Tension += Time.deltaTime;
            }
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
        }
    }

    IEnumerator RopeReturn(){
        Vector3 startLocalPosition = RopeTransform.localPosition;
        for(float f = 0; f < 1f; f += Time.deltaTime / ReturnTime) { 
        RopeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RopeNearLocalPosition,  RopeReturnAnimation.Evaluate(f));
            yield return null;
        }
        RopeTransform.localPosition = RopeNearLocalPosition;
    }
}