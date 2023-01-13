using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private BoxCollider2D _CameraCollider;
    private Camera _GameCamera;
    private float cameraHeight;
    private float cameraWidth;
    void Start()
    {

        UpdateCameraColliderSize();
    }

    public void UpdateCameraColliderSize()
    
    {
        _CameraCollider = GetComponent<BoxCollider2D>();
        _GameCamera = GetComponent<Camera>();
        cameraWidth = _GameCamera.orthographicSize * _GameCamera.aspect * 2;
        cameraHeight = _GameCamera.orthographicSize * 2;

        _CameraCollider.size = new Vector2(cameraWidth, cameraHeight);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
