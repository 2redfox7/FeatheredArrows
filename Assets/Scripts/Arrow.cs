using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] float arrowSpeed;

    private Rigidbody arrowRigidbody;

    public TrailRenderer TrailRenderer;

    public AudioSource HitSound;
    public AudioSource EnemySound;

    private bool isActivate = true;

    Vector3 mouseWorldPosition = Vector3.zero;

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
        if (collision.gameObject.tag == "Target")
        {
            HitSound.pitch = UnityEngine.Random.Range(0.95f, 1.1f);
            HitSound.Play();
            collision.gameObject.SetActive(false);
            TrailRenderer.enabled = false;
            arrowRigidbody.transform.position = new(0, 0, 0);
            Rating.score += 100;

        }
        if (collision.gameObject.tag == "Enemy")
        {
            HitSound.pitch = UnityEngine.Random.Range(0.95f, 1.1f);
            EnemySound.Play();
            Destroy(collision.gameObject);
            TrailRenderer.enabled = false;
            arrowRigidbody.transform.position = new(0, 0, 0);
            Rating.score += 500;
        }
        if (collision.gameObject.tag == "Environment")
        {
            arrowRigidbody.isKinematic = true;
            TrailRenderer.enabled = false;
            BowRope.fly = false;
        }
        arrowRigidbody.isKinematic = true;
        BowRope.fly = false;
        isActivate = false;
    }

    public void SetToRope(Transform ropeTransform)
    {
            isActivate = true;
            transform.parent = null;
            transform.parent = ropeTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            arrowRigidbody.isKinematic = true;
            TrailRenderer.enabled = false;
    }
    public void Shot(float velocity)
    {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
            }
            Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;
            transform.parent = null;
            arrowRigidbody.velocity = transform.forward * velocity;
            arrowRigidbody.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
            arrowRigidbody.isKinematic = false;
            TrailRenderer.Clear();
            TrailRenderer.enabled = true;
    }
}