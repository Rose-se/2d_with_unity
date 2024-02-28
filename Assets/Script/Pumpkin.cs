using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {

            gameObject.SetActive(false);
        }
    }
}
