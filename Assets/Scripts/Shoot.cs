using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float arrowSpeed;

    private Rigidbody arrowRigidbody;
    
    public TrailRenderer TrailRenderer;

    public AudioSource HitSound;
    public AudioSource EnemySound;

    private bool isActivate = true;


    private void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
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

        arrowRigidbody.isKinematic = true;
        TrailRenderer.enabled = false;
    }
    public void Shot(float velocity)
    {
        transform.parent.parent = null;
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.velocity = transform.forward * velocity;
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }
}
