using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship
{
    public float ShipHealth { get; private set; }

    /// <summary>
    /// List of all available attachments, used to populate the attachment menu
    /// </summary>
    public List<Attachment> Attachments { get; private set; }

    private FuelController fuelController;
    private AttachmentsController attachmentsController;
    private Pilot pilot;

    public Ship(FuelController fuelController, AttachmentsController attachmentsController, Pilot pilot)
    {
        this.fuelController = fuelController;
        this.attachmentsController = attachmentsController;
        this.pilot = pilot;

        Attachments = new List<Attachment>();
    }

    /// <summary>
    /// When requesting status, use this rather than interacting directly with fuel controller script.
    /// </summary>
    /// <returns>Current Fuel Level (float)</returns>
    public float GetFuelLevel()
    {
        return fuelController.CurrentFuelLevel;
    }

    /// <summary>
    /// Use when the player discovers new attachment and it should be added to their availble attachment list.
    /// </summary>
    /// <param name="attachment">Send the new attachment from the game level complete attachment list.</param>
    public void AddAvailableAttachment(Attachment attachment)
    {
        Attachments.Add(attachment);
    }



    /// <summary>
    /// Removes the availability of an attachment. Checks if key bound and removes binding.
    /// </summary>
    /// <param name="attachment">Send the attachment to remove.</param>
    public void RemoveAvailableAttachment(Attachment attachment)
    {
        Attachments.Remove(attachment);

        Dictionary<KeyCode, Attachment> newKeyBindings;

        newKeyBindings = pilot.AttachmentKeyBindings;

        foreach (Attachment attachmentToRemove in newKeyBindings.Values)
        {
            if (attachmentToRemove == attachment)
            {
                KeyCode keyToRemove = UtilityFunctions.FindKeyByValue(newKeyBindings, attachment);
                if (!keyToRemove.Equals(default(KeyCode)))
                {
                    newKeyBindings.Remove(keyToRemove);
                }
            }
        }

        UpdateAttachmentKeyBindings(newKeyBindings);

    }


    private void UpdateAttachmentKeyBindings(Dictionary<KeyCode, Attachment> newKeyBindings)
    {
        pilot.UpdateAttachmentKeyBindings(newKeyBindings);
    }

}
