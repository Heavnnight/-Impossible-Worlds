using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform character;

    [Header("Mouse Settings")]
    public float sensitivity = 2f;
    public float smoothing = 1.5f;

    [Header("Head Bob Settings")]
    public float bobSpeed = 10f;
    public float bobAmountX = 0.02f;
    public float bobAmountY = 0.02f;

    Vector2 velocity;
    Vector2 frameVelocity;

    Vector3 originalPos;

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Save original camera position
        originalPos = transform.localPosition;
    }

    void Update()
    {
        // Mouse input
        Vector2 mouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        // Smooth mouse movement
        Vector2 rawFrameVelocity = Vector2.Scale(
            mouseDelta,
            Vector2.one * sensitivity
        );

        frameVelocity = Vector2.Lerp(
            frameVelocity,
            rawFrameVelocity,
            1 / smoothing
        );

        velocity += frameVelocity;

        // Prevent camera from flipping backwards
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        // Camera rotation
        transform.localRotation = Quaternion.AngleAxis(
            -velocity.y,
            Vector3.right
        );

        // Character rotation
        character.localRotation = Quaternion.AngleAxis(
            velocity.x,
            Vector3.up
        );

        // Movement check
        float moveAmount =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) +
            Mathf.Abs(Input.GetAxisRaw("Vertical"));

        // Head bob effect
        if (moveAmount > 0)
        {
            float bobX = Mathf.Sin(Time.time * bobSpeed) * bobAmountX;
            float bobY = Mathf.Cos(Time.time * bobSpeed * 2f) * bobAmountY;

            transform.localPosition = originalPos + new Vector3(bobX, bobY, 0);
        }
        else
        {
            // Smoothly return to original position
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalPos,
                Time.deltaTime * 5f
            );
        }
    }
}