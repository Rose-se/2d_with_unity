using UnityEngine;

public class FollowCharacterFrontWithTransformAndSprite : MonoBehaviour
{
    [SerializeField] private Transform mainCharacterTransform;
    [SerializeField] private SpriteRenderer mainCharacterSpriteRenderer;
    [SerializeField] private float distanceInFront = 1.0f;

    void Start()
    {
        // ตรวจสอบว่า mainCharacterTransform และ mainCharacterSpriteRenderer ถูกกำหนดให้ถูกต้อง
        if (mainCharacterTransform == null)
        {
            Debug.LogError("Main Character Transform is not assigned!");
        }

        if (mainCharacterSpriteRenderer == null)
        {
            Debug.LogError("Main Character SpriteRenderer is not assigned!");
        }
    }

    void Update()
    {
        // คำนวณตำแหน่งที่ต้องการให้ GameObject อยู่ข้างหน้าตัวละคร
        Vector2 frontPosition = mainCharacterTransform.position + new Vector3(mainCharacterSpriteRenderer.flipX ? -distanceInFront : distanceInFront, 0, 0);

        // ให้ GameObject ติดตามตำแหน่งที่คำนวณไว้
        transform.position = frontPosition;

        // ตั้งค่าหันให้ GameObject มีทิศทางเดียวกับตัวละคร
        transform.localScale = new Vector3(mainCharacterSpriteRenderer.flipX ? -1 : 1, 1, 1);
    }
}
