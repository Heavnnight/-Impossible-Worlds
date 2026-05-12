using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;

    private void Respawn()
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        if (controller != null)
            controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            Respawn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
            Respawn();
    }
}