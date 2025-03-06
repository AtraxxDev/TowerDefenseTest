using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float mouseSensitivity = 2f;

    private float rotationX = 0f;
    private bool isRotating = false;



    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);

        if (Input.GetMouseButtonDown(1)) 
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
            isRotating = true; 

        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
            isRotating = false; 
        }

        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * mouseSensitivity * Time.deltaTime;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -80f, 80f);

            transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y + mouseX, 0f);
        }
    }
}
