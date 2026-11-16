using System.Collections;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float speedMultiplier = 0.7f;
    public float duration = 3f;
    public string message = "You're caught in a spider web!";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            StartCoroutine(SlowPlayer(player));

            FindFirstObjectByType<GameManager>().ShowMessage(message, duration);
        }
    }

    IEnumerator SlowPlayer(PlayerController player)
    {
        player.speedMultiplier = speedMultiplier;
        yield return new WaitForSeconds(duration);
        player.speedMultiplier = 1f;
    }
}