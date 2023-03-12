using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public TrailRenderer TrailRenderer;

    public AudioSource HitSound;
    public AudioSource EnemySound;

    private bool isActivate = true;

    public void OnCollisionEnter(Collision collision)
    {
        if (!isActivate) return;
        if (collision.gameObject.name == "Target")
        {
            HitSound.pitch = UnityEngine.Random.Range(0.95f, 1.1f);
            HitSound.Play();
            collision.gameObject.SetActive(false);
            TrailRenderer.enabled = false;
            transform.parent.position = new(0, 0, 0);
            Rating.score += 100;

        }
        if (collision.gameObject.tag == "Enemy")
        {
            HitSound.pitch = UnityEngine.Random.Range(0.95f, 1.1f);
            EnemySound.Play();
            Destroy(collision.gameObject);
            TrailRenderer.enabled = false;
            transform.parent.position = new(0, 0, 0);
            Rating.score += 500;
        }
        if (collision.gameObject.tag == "Environment")
        {
            BowRope.fly = false;
        }
        BowRope.fly = false;
        isActivate = false;
    }
    public void SetToRope(Transform ropeTransform)
    {
        isActivate = true;
        transform.parent.parent = null;
        transform.parent.parent = ropeTransform;
        transform.parent.localPosition = Vector3.zero;
        transform.parent.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody.isKinematic = true;
        TrailRenderer.enabled = false;
    }
    public void Shot()
    {
        transform.parent.parent = null;
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }

}
