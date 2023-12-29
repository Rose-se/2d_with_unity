using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField] private float xSpeed = 2f;
    [SerializeField] private float ySpeed = 1.5f; // Adjust this value to control the Y-axis movement speed
    [SerializeField] private Transform target;

    void Update()
    {
        float targetX = Mathf.Lerp(transform.position.x, target.position.x, xSpeed * Time.deltaTime);
        float targetY = Mathf.Lerp(transform.position.y, target.position.y, ySpeed * Time.deltaTime);

        Vector3 targetPosition = new Vector3(targetX, targetY, -10f);

        transform.position = targetPosition;
    }
}
