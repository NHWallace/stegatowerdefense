using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderVertical = (Screen.height * 0.05f); // # of pixels away a mouse has to be from the top or bottom of the screen to pan the camera
    public float panBorderHorizontal = (Screen.width * 0.05f);

    public float scrollSpeed = 5f;
    public float minY = 20f;
    public float maxY = 80f;
    public float minX = -10f;
    public float maxX = 100f;
    public float minZ = -20f;
    public float maxZ = 100f;

    private void Update()
    {

        if (GameManager.GameEnded)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderVertical)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderVertical)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderHorizontal)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderHorizontal)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 newPosition = transform.position;

        newPosition.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}
