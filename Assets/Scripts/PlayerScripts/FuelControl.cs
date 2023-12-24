using System.Collections;
using UnityEngine;

public class FuelControl : MonoBehaviour
{

    [SerializeField] private float maxFuelLevel = 3f;
    [SerializeField] private float refuelTime = 2f;
    [SerializeField] private float currentFuelLevel;
    private bool isRefueling;

    public float CurrentFuelLevel
    {
        get { return currentFuelLevel; }
        private set { currentFuelLevel = Mathf.Clamp(value, 0f, maxFuelLevel); }
    }

    public bool IsRefueling
    {
        get { return isRefueling; }
        private set { isRefueling = value; }
    }


    public void StartRefuel()
    { 
        if (CurrentFuelLevel < 0.1f && !IsRefueling)
        {
            StartCoroutine(RefuelCoroutine(refuelTime));
        }
       
    }


    public void ConsumeFuel(float amount)
    {
        CurrentFuelLevel -= amount;
       
    }


    private IEnumerator RefuelCoroutine(float waitTime)
    {
        IsRefueling = true;
        yield return new WaitForSeconds(waitTime);
        CurrentFuelLevel = maxFuelLevel;
        IsRefueling = false;
    }
}
