using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTorque : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float VelocityMult;

    
    public float AngularVelocityMult;
    void FixedUpdate()
    {
        //v.x = (float)(v0 * Math.Cos(fi));
        //v.y = (float)(v0 * Math.Sin(fi));
        //VerletPredKorr(g, ref r, ref v, vmax, wind);
        //double vmod = modul(v);
        //Vector3 cross = Vector3.Cross(transform.forward, Rigidbody.velocity.normalized);
        //Rigidbody.AddTorque(cross * Rigidbody.velocity.magnitude * VelocityMult);
        //Rigidbody.AddTorque((- Rigidbody.angularVelocity + Vector3.Project(Rigidbody.angularVelocity, transform.forward)) * Rigidbody.velocity.magnitude * AngularVelocityMult);
        //double tempx = Math.Pow(vmod, 2) / modul(g) * Math.Sin(2 * fi);
        //double tempy = Math.Pow(vmod * Math.Sin(fi), 2) / (2 * modul(g));
        //Vector3 newPosition = new Vector3(0, (float)tempy, (float)tempx);
        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.time / 0.5f);
    }
}
