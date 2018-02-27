using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    public float smoothTime = .5f;

    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Use this for initialization
    float GetGreaterDistance() {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    void Zoom() {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreaterDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

    }
    void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
