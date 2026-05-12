using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9f;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Drunk Effect")]
    public bool drunkMovement = true;

    // قوة الترنح
    public float swayAmount = 2f;

    // سرعة الترنح
    public float swaySpeed = 4f;

    // اهتزاز زيادة
    public float randomShake = 0.4f;

    Rigidbody rigidbody;

    public List<System.Func<float>> speedOverrides =
        new List<System.Func<float>>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Running
        IsRunning = canRun && Input.GetKey(runningKey);

        // Speed
        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed =
                speedOverrides[speedOverrides.Count - 1]();
        }

        // Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // DRUNK SWAY
        if (drunkMovement)
        {
            float sway =
                Mathf.Sin(Time.time * swaySpeed) * swayAmount;

            float shake =
                Random.Range(-randomShake, randomShake);

            
            moveX += sway + shake;

            moveZ += Mathf.Cos(Time.time * 2f) * 0.3f;
        }

        // Apply movement
        Vector3 move =
            transform.rotation *
            new Vector3(
                moveX * targetMovingSpeed,
                rigidbody.linearVelocity.y,
                moveZ * targetMovingSpeed
            );

        rigidbody.linearVelocity = move;
    }
}