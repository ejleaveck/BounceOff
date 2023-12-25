using UnityEngine;

public class PulseEngineController : MonoBehaviour
{

    private float pulseMultiplier;
    private float pulseOffMultiplierDefault = 1f;
    [SerializeField] private float pulseOnMultiplierDefault = 4f;

    [SerializeField] private float pulseBurnRate = 2f;
    private bool isPulseOn;

    private FuelController fuelControl;


    //Based on environmental effects, NOT Fuel rate
    private bool isPulseAvailable;


    /// <summary>
    /// Set according to environmental effects.
    /// NOT used for fuel level
    /// </summary>
    public bool IsPulseAvailable
    {
        get { return isPulseAvailable; }
        set { isPulseAvailable = value; }
    }

    public float PulseMultiplier
    {
        get { return pulseMultiplier; }
        private set { pulseMultiplier = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        fuelControl = GetComponent<FuelController>();
    }

    private void Update()
    {
        if (isPulseOn && isPulseAvailable && fuelControl.CurrentFuelLevel > 0)
        {
            fuelControl.ConsumeFuel(pulseBurnRate);
            PulseMultiplier = pulseOnMultiplierDefault;
        }
        else
        {
            isPulseOn = false;
            PulseMultiplier = pulseOffMultiplierDefault;

        }
    }

    /// <summary>
    /// Decrements Fuel when fired.
    /// C
    /// </summary>
    /// <param name="tryTurningOn"></param>
    /// <returns></returns>
    public void SetPulseEngineState(bool tryTurningOn)
    {
        if (tryTurningOn)
        {
            isPulseOn = true;
        }
        else
        {
            isPulseOn = false;
        }
    }


}
