using UnityEngine;
using UnityEngine.EventSystems; // Necessário para IPointerEnterHandler

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AkSoundEngine.PostEvent("menu_cursor_move", gameObject);
    }
}
