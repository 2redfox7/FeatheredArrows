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

    public Arrow CurrentArrow;

    public AudioSource BowTension;
    public AudioSource ArrowWhistling;

    public Vector3 r = new(0f, 0f, 0f);
    public Vector3 v = new();
    public Vector3 g = new(0, 9.81f, 0);
    public Vector3 wind = new();

    public double v0;
    public float fi;
    public float windx, windy, windz, h;
    public double vmax = 150;

    public double Modul(Vector3 a)
    { return Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z); }
    private Vector3 Accel(Vector3 g, Vector3 v, double vmax, Vector3 wind, double h)
    {
        return g - (float)(Math.Exp(-h / 10000) * Modul(g) * Modul(v - wind) / (vmax * vmax)) * (v - wind);
    }
    private void VerletPredKorr(Vector3 g, ref Vector3 r, ref Vector3 v, double vmax, Vector3 wind)
    {
        Vector3 a;
        Vector3 vPred;
        a = Accel(g, v, vmax, wind, r.y);
        r += v * Time.deltaTime + (float)0.5 * Time.deltaTime * Time.deltaTime * a;
        vPred = v + a * Time.deltaTime;
        v += (float)0.5 * Time.deltaTime * a;
        a = Accel(g, vPred, vmax, wind, r.y);
        v += (float)0.5 * Time.deltaTime * a;
    }
    void Start()
    {
        RopeNearLocalPosition = RopeTransform.localPosition;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressed = true;

            r = new Vector3(0f, 0f, 0f);
            v = new Vector3(0f, 0f, 0f);
            CurrentArrow.SetToRope(RopeTransform);

            BowTension.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            BowTension.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            fly = true;
            _pressed = false;
            StartCoroutine(RopeReturn());
            CurrentArrow.Shot();
            CurrentArrow.transform.localEulerAngles = new Vector3(CurrentArrow.transform.parent.eulerAngles.x, 0, CurrentArrow.transform.parent.eulerAngles.z);
            CurrentArrow.transform.parent.eulerAngles = new Vector3(0, CurrentArrow.transform.parent.eulerAngles.y, 0);
            fi = -CurrentArrow.transform.localEulerAngles.x;
            fi *= Mathf.Deg2Rad;
            v.x = (float)(v0 * Tension * Mathf.Cos(fi));
            v.y = (float)(v0 * Tension * Mathf.Sin(fi));
            wind.x = windx;
            wind.y = windy;
            wind.z = windz;

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

    void FixedUpdate()
    {
        if (!_pressed && fly)
        {
            VerletPredKorr(g, ref r, ref v, vmax, wind);
            Vector3 NextPos = new(CurrentArrow.transform.localPosition.x, r.y, r.x);
            CurrentArrow.transform.rotation = Quaternion.LookRotation(-(CurrentArrow.transform.localPosition - NextPos));
            CurrentArrow.transform.eulerAngles += new Vector3(0, CurrentArrow.transform.parent.eulerAngles.y, 0);
            CurrentArrow.transform.localPosition = NextPos;
        }
    }
}
