using UnityEngine;

public class DoofusMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");  

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Move Doofus
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}