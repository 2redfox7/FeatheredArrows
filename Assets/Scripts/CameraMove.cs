using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float MouseSensitivity = .25f;

    private Vector3 _mousePreveousePos;
    private Vector3 _mouseDelta;

    [Header("Sitting")]
    public float StandingHeight = 1.5f;
    public float SittingHeight = 0.5f;
    public float SittingTime = .1f;
    // Sitting
    private float _yPosCurrent;
    private float _yPosTarget;
    private float _yPosRef;
    private bool _isSitting;
    
    public float WalkSpeed = 2f;
    public bool RotateWithoutButton;

    void Awake() {
        _yPosTarget = StandingHeight;
    }

    void Update() {
        Move();
        Rotate();
    }

    void Move() {

        float speed = Time.deltaTime * WalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed *= 2.5f;

        Vector3 forwardProjection = new Vector3(transform.forward.x, 0f, transform.forward.z);
        Vector3 rightProjection = new Vector3(transform.right.x, 0f, transform.right.z);
        
        if (Input.GetKey(KeyCode.W)) transform.position += forwardProjection.normalized * speed;
        if (Input.GetKey(KeyCode.S)) transform.position -= forwardProjection.normalized * speed;
        if (Input.GetKey(KeyCode.A)) transform.position -= rightProjection.normalized * speed;
        if (Input.GetKey(KeyCode.D)) transform.position += rightProjection.normalized * speed;

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            _isSitting = !_isSitting;
            _yPosTarget = _isSitting ? SittingHeight : StandingHeight;
        }
        
        _yPosCurrent = Mathf.SmoothDamp(_yPosCurrent, _yPosTarget, ref _yPosRef, SittingTime);
        transform.position = new Vector3(transform.position.x, _yPosCurrent, transform.position.z);
    }

    void Rotate() {
        if (Input.GetMouseButtonDown(1)) {
            _mousePreveousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1) || RotateWithoutButton) {
            _mouseDelta = Input.mousePosition - _mousePreveousePos;
            _mousePreveousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1)) {
            _mouseDelta = Vector3.zero;
        }

        transform.Rotate(-transform.right, _mouseDelta.y * MouseSensitivity, Space.World);
        transform.Rotate(Vector3.up, _mouseDelta.x * MouseSensitivity, Space.World);
    }
}
