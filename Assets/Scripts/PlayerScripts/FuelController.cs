using UnityEngine;

public class FuelController : MonoBehaviour
{
    [SerializeField] private float currentFuelLevel = 10f;
    [SerializeField] private float maxFuelLevel = 10f;

    /// <summary>
    /// 1 = 1 second of fuel per second
    /// Cannot be zero.
    /// </summary>
    [SerializeField] private float _refuelRate = 1f;
    [SerializeField] private float refuelDelay = 1f;

    private float refuelTimer = 0f;
    private bool isConsumingFuel = false;

    private float emptyTankThreshold = 0f;

    public bool CanRefuel { get; set; } = true;

    /// <summary>
    /// Use to keep empty tank logic in fuel controller
    /// Use this to check tank instead of current fuel level
    /// </summary>
    public bool IsFuelTankEmpty { get; set; }

    /// <summary>
    /// Tracks Fuel Level as an absolute value.
    /// Use to check if a Fuel Consuming action can activate.
    /// </summary>
    public float CurrentFuelLevel
    {
        get { return currentFuelLevel; }
        private set { currentFuelLevel = Mathf.Clamp(value, 0f, MaxFuelLevel); }
    }

    public float MaxFuelLevel
    {
        get { return maxFuelLevel; }
        set { maxFuelLevel = value; }
    }

    /// <summary>
    /// Set rate in seconds. 1 refuelRate = 1 FuelLevel per second.
    /// </summary>
    public float RefuelRate
    {
        get { return _refuelRate; }
        set
        {
            if (value <= 0f)
            {
                Debug.LogError("RefuelRate must be greater than zero. Adjust CanRefuel instead. Rate set to .1f.");
                _refuelRate = .1f;
            }
            else
            {
                _refuelRate = value;
            }
        }
    }

    private void Awake()
    {        
        //CurrentFuelLevel = maxFuelLevel;
    }


    private void FixedUpdate()
    {
        if (isConsumingFuel)
        {
            refuelTimer = refuelDelay;
        }
        else
        {
            if (refuelTimer > 0f)
            {
                refuelTimer -= Time.deltaTime;
            }
            else if (CanRefuel && CurrentFuelLevel < maxFuelLevel)
            {
                Refuel();
            }
        }

        IsFuelTankEmpty = CurrentFuelLevel <= emptyTankThreshold;
    }

    public void ConsumeFuel(float burnRate)
    {
        isConsumingFuel = true;
        CurrentFuelLevel -= burnRate * Time.deltaTime;
    }

    private void Refuel()
    {
        if (RefuelRate > 0f)
        {
            CurrentFuelLevel += RefuelRate * Time.deltaTime;
            CurrentFuelLevel = Mathf.Clamp(currentFuelLevel, 0f, MaxFuelLevel);
        }
    }

    public void StopConsumingFuel()
    {
        isConsumingFuel = false;
    }


}
