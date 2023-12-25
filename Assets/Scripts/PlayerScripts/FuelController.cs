using System.Collections;
using UnityEngine;

public class FuelController : MonoBehaviour
{

    private float maxFuelLevel = 5f;
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

    public float MaxFuelLevel
    {
        get { return maxFuelLevel; }
        set { maxFuelLevel = value; }
    }    

    public void StartRefuel()
    { 
        if (CurrentFuelLevel < 0.1f && !IsRefueling)
        {
            StartCoroutine(RefuelCoroutine(refuelTime));
        }
       
    }

    /// <summary>
    /// Decrements fuel level according to burn rate
    /// </summary>
    /// <param name="amount">Fuel BurnRate</param>
    public void ConsumeFuel(float burnRate)
    {
        CurrentFuelLevel -= (burnRate *Time.deltaTime);
       
    }


    private IEnumerator RefuelCoroutine(float waitTime)
    {
        IsRefueling = true;
        yield return new WaitForSeconds(waitTime);
        CurrentFuelLevel = maxFuelLevel;
        IsRefueling = false;
    }
}
