using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider2D boundsCollider; 

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        Bounds bounds = boundsCollider.bounds;

        float mapWidth = bounds.size.x;
        float mapHeight = bounds.size.y;

        float sizeByHeight = mapHeight / 2f;
        float sizeByWidth = (mapWidth / 2f) / cam.aspect;

        cam.orthographic = true;
        cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth);

        transform.position = new Vector3(bounds.center.x, bounds.center.y, transform.position.z);
    }
}