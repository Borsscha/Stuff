using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;              // Player
    public Vector2 offset = Vector2.zero; // XY-Offset in Weltkoordinaten
    public float smoothTime = 0.15f;      // 0 = sofortig, höher = weicher

    private Vector3 _velocity;            // für SmoothDamp
    private float _fixedZ;                // Kamera-Z bleibt konstant

    void Awake()
    {
        _fixedZ = transform.position.z;   // z.B. -10
        // Optional für 2D:
        // GetComponent<Camera>().orthographic = true;
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desired = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            _fixedZ
        );

        // Weiches, fps-stabiles Folgen:
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desired,
            ref _velocity,
            smoothTime
        );
    }
}
