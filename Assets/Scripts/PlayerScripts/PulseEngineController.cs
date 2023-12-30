using UnityEngine;

public class PulseEngineController : MonoBehaviour
{

    private float pulseOffMultiplierDefault = 1f;
    [SerializeField] private float pulseOnMultiplierDefault = 4f;

    [SerializeField] private float pulseBurnRate = 2f;
    private bool isPulseOn;

    private FuelController fuelControl;

    /// <summary>
    /// Set according to environmental effects.
    /// NOT used for fuel level
    /// </summary>
    public bool IsPulseAvailable { get; set; }


    /// <summary>
    /// Use to access current Pulse Speed boost based on game factors.
    /// </summary>
    public float PulseMultiplier { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        fuelControl = GetComponent<FuelController>();
    }

    private void FixedUpdate()
    {
        if (isPulseOn && IsPulseAvailable && !fuelControl.IsFuelTankEmpty)
        {
            fuelControl.ConsumeFuel(pulseBurnRate);
            PulseMultiplier = pulseOnMultiplierDefault;
        }
        else
        {
            SetPulseEngineState(false);
            PulseMultiplier = pulseOffMultiplierDefault;
        }
    }


    public void SetPulseEngineState(bool isActive)
    {
        //keep this method incase I want to do something when it turns on or off like a thruster animation.
        isPulseOn = isActive;
    }


}
