using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab; // Reference to the Pulpit prefab
    public float minPulpitLifetime =4f; // Minimum time a Pulpit lasts
    public float maxPulpitLifetime = 5f; // Maximum time a Pulpit lasts

    private List<GameObject> pulpits = new List<GameObject>(); // List to keep track of existing Pulpits
    private GameObject lastPulpit; // Reference to the last spawned Pulpit

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnPulpit());
    }

    IEnumerator SpawnPulpit()
    {
        while (true)
        {
            // Ensure only two Pulpits exist at any time
            if (pulpits.Count < 2)
            {
                // Determine the position for the new Pulpit
                Vector3 newPosition = GetNewPulpitPosition();
                GameObject newPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);
                pulpits.Add(newPulpit);

                // Set the lifetime for the Pulpit
                float pulpitLifetime = Random.Range(minPulpitLifetime, maxPulpitLifetime);
                Destroy(newPulpit, pulpitLifetime);

                // Set this as the last Pulpit
                lastPulpit = newPulpit;

                // Remove the Pulpit from the list after its lifetime
                StartCoroutine(RemovePulpitFromListAfterTime(newPulpit, pulpitLifetime));
            }

            // Wait for a short delay before trying to spawn the next Pulpit
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RemovePulpitFromListAfterTime(GameObject pulpit, float time)
    {
        yield return new WaitForSeconds(time);
        pulpits.Remove(pulpit);
    }

    Vector3 GetNewPulpitPosition()
    {
        // If this is the first Pulpit, place it at the origin
        if (lastPulpit == null)
            return Vector3.zero;

        // Determine the new Pulpit's position relative to the last one
        Vector3[] possibleOffsets = new Vector3[] {
            new Vector3(9, 0, 0), // Right
            new Vector3(-9, 0, 0), // Left
            new Vector3(0, 0, 9), // Forward
            new Vector3(0, 0, -9) // Backward
        };

        Vector3 offset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];
        Vector3 newPosition = lastPulpit.transform.position + offset;

        // Ensure the new position is not occupied by another Pulpit
        while (IsPositionOccupied(newPosition))
        {
            offset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];
            newPosition = lastPulpit.transform.position + offset;
        }

        return newPosition;
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (GameObject pulpit in pulpits)
        {
            if (pulpit != null && pulpit.transform.position == position)
                return true;
        }
        return false;
    }
}