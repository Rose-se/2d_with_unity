using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform target;

    void Update()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // Check if the target is moving left or right
        if (Mathf.Abs(target.position.x - transform.position.x) > 2.5f)
        {
            targetX = Mathf.Lerp(transform.position.x, target.position.x, moveSpeed * Time.deltaTime);
        }

        // Check if the target is moving up or down
        if (Mathf.Abs(target.position.y - transform.position.y) > 10f)
        {
            targetY = Mathf.Lerp(transform.position.y, target.position.y, moveSpeed * Time.deltaTime);
        }

        Vector3 targetPosition = new Vector3(targetX, targetY, -15f);

        transform.position = targetPosition;
    }
}
