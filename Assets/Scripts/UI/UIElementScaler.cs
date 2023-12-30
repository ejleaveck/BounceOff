using UnityEngine;
using UnityEngine.EventSystems;

public class UIElementScaler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = selectedScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
