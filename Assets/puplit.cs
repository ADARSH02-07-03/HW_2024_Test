using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puplit : MonoBehaviour
{
    public float minTime = 4f;
public float maxTime = 5f;

private void Start()
{
    float timer = Random.Range(minTime, maxTime);
    Invoke("DestroyPulpit", timer);
}

void DestroyPulpit()
{
    Destroy(gameObject);
}
}
