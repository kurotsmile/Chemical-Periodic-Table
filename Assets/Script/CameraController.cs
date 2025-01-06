using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 4.7f;
    public float zoomSpeed = 2.0f;
    public float rotationSpeed = 100.0f;
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    public float initialRotationX = 20.0f;
    public float initialRotationY = 35.0f; 

    private float currentX;
    private float currentY;
    private float initialDistance = 0.0f;

    void Start()
    {
        currentX = initialRotationX;
        currentY = initialRotationY;
    }

    void Update()
    {
        // Kiểm tra đầu vào từ chuột (PC)
        if (Input.GetMouseButton(0)) // Nhấn chuột phải để xoay
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, -80f, 80f); // Giới hạn góc xoay dọc
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                currentX += touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                currentY -= touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
                currentY = Mathf.Clamp(currentY, -80f, 80f); // Giới hạn góc xoay dọc
            }
        }

        // Kiểm tra zoom (PC hoặc mobile)
        if (Input.touchCount == 2) // Hai ngón tay dùng để zoom
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (initialDistance == 0)
                initialDistance = currentDistance;

            float distanceDelta = currentDistance - initialDistance;
            distance -= distanceDelta * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minDistance, maxDistance); // Giới hạn zoom

            initialDistance = currentDistance;
        }
        else
        {
            initialDistance = 0;
        }

        // Cuộn chuột (PC)
        if (Input.mouseScrollDelta.y != 0)
        {
            distance -= Input.mouseScrollDelta.y * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance); // Giới hạn zoom
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Tính toán vị trí camera
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 direction = new Vector3(0, 0, -distance);
            transform.position = target.position + rotation * direction;

            // Camera luôn nhìn vào mục tiêu
            transform.LookAt(target);
        }
    }
}
