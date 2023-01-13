using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    private Transform player;
    private BoxCollider2D _CameraArea;
    private BoxCollider2D _BoundaryBox;
    // Start is called before the first frame update
    void Start()
    {
        _CameraArea = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Boundary"))
        {
            _BoundaryBox = GameObject.Find("Boundary").GetComponent<BoxCollider2D>();

            transform.position = new Vector3(Mathf.Clamp(player.position.x, _BoundaryBox.bounds.min.x + _CameraArea.size.x / 2f, _BoundaryBox.bounds.max.x - _CameraArea.size.x / 2f),
                                                Mathf.Clamp(player.position.y, _BoundaryBox.bounds.min.y + _CameraArea.size.y / 2f, _BoundaryBox.bounds.max.y - _CameraArea.size.y / 2f), -1f);

        }
        else
        {
            Debug.Log("CameraOutOFBounds");
            transform.position = transform.position;
        }
    }
}
