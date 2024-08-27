using UnityEngine;
using TMPro;

public class PulpitTimer : MonoBehaviour
{
    public float pulpitLifetime = 5f; // The time this Pulpit will last
    private float timer; // Timer to track the remaining time
    private TextMeshPro textMeshPro; // Reference to the TextMeshPro component

    void Start()
    {
        // Initialize the timer with the Pulpit's lifetime
        timer = pulpitLifetime;

        // Find the TextMeshPro component in the child object
        textMeshPro = GetComponentInChildren<TextMeshPro>();

        // Set the initial timer text
        UpdateTimerText();
    }

    void Update()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Update the timer text
        UpdateTimerText();

        // Destroy the Pulpit when the timer runs out
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void UpdateTimerText()
    {
        // Update the text with the remaining time (formatted to one decimal place)
        if (textMeshPro != null)
        {
            textMeshPro.text = timer.ToString("F1");
        }
    }
}