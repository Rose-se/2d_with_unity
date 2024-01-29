using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        // เพิ่ม Entry สำหรับ PointerClick
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { PlayButtonClickSound(); });

        // เพิ่ม Entry เข้า EventTrigger
        eventTrigger.triggers.Add(entry);
    }

    // ฟังก์ชันที่ถูกเรียกเมื่อปุ่มถูกคลิก
    private void PlayButtonClickSound()
    {
        if (audioSource != null && audioSource.enabled)
        {
            audioSource.Play();
        }
    }

    // ฟังก์ชันที่ถูกเรียกเมื่อปุ่มถูกคลิก (implement จาก IPointerClickHandler)
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayButtonClickSound();
    }
}
