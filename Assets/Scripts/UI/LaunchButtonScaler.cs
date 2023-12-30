using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchButtonScaler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float scalingSpeed = 2f; // Speed of the scaling effect

    private Vector3 originalScale;
    private bool isSelected = false;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isSelected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, selectedScale, scalingSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scalingSpeed * Time.deltaTime);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
    }
}
