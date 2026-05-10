using UnityEngine;

public class FollowOrFlee : MonoBehaviour
{
    [Header("Target & Mode")]
    public Transform player; // Drag your player here in the Inspector
    public bool runAway = false; // Check this box to make it flee instead of follow
    public float speed = 5f;

    [Header("Distance Settings")]
    public float followStoppingDistance = 2f; // How close it gets before stopping
    public float fleeStartDistance = 10f; // It will only run away if the player gets closer than this

    private void Update()
    {
        // Safety check to make sure the player is assigned
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (runAway)
        {
            FleeFromPlayer(distanceToPlayer);
        }
        else
        {
            FollowPlayer(distanceToPlayer);
        }
    }

    private void FollowPlayer(float distance)
    {
        // Only move if we are further than the stopping distance
        if (distance > followStoppingDistance)
        {
            // Calculate the direction TOWARDS the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the object
            transform.position += direction * speed * Time.deltaTime;

            // Make the object face the player
            transform.LookAt(player.position);
        }
    }

    private void FleeFromPlayer(float distance)
    {
        // Only run away if the player is dangerously close
        if (distance < fleeStartDistance)
        {
            // Calculate the direction AWAY from the player
            Vector3 direction = (transform.position - player.position).normalized;

            // Move the object
            transform.position += direction * speed * Time.deltaTime;

            // Make the object face the direction it is running
            transform.LookAt(transform.position + direction);
        }
    }
}