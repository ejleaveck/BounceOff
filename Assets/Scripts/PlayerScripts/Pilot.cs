using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Pilot 
{
    public string Name { get; private set; }
    public string BackStory { get; private set; }
    public ColorScheme PilotColorScheme { get; private set; }
   
        public enum ColorScheme
        {
           RusticBlue,
           HardyRed
        }

    public int Level { get; private set; }
    public int ExperiencePoints { get; private set; }


    /// <summary>
    /// List of all available attachments, used to populate the attachment menu
    /// </summary>
    public List<Attachment> Attachments { get; private set; }


    /// <summary>
    /// Stores the button KeyCode with Attachment definition for use during play
    /// </summary>
    public Dictionary<KeyCode, Attachment> AttachmentKeyBindings { get; private set; }
    
    /// <summary>
    /// Default Settings
    /// </summary>
    /// <param name="name"></param>
    /// <param name="backStory"></param>
    protected Pilot(string name, string backStory)
    {
        Name = name;
        BackStory = backStory;
        PilotColorScheme = ColorScheme.RusticBlue;
        Level = 1;
        ExperiencePoints = 0;

        Attachments = new List<Attachment>();
        AttachmentKeyBindings = new Dictionary<KeyCode, Attachment>();

        
    }


    /// <summary>
    /// Adust the player to current level.
    /// </summary>
    /// <param name="adjustment">Positive to increment; negative to decrement</param>
    public void AdjustPilotLevel(int adjustment)
    {
        Level = Level + adjustment;
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

        foreach (Attachment attachmentToRemove in AttachmentKeyBindings.Values)
        {
            if(attachmentToRemove == attachment)
            {
                KeyCode keyToRemove = FindKeyByValue(AttachmentKeyBindings, attachment);

                AttachmentKeyBindings.Remove(keyToRemove);
            }
        }
    }

    public void UpdateAttachmentKeyBindings(Dictionary<KeyCode, Attachment> attachmentKeyBindings)
    {
        AttachmentKeyBindings = attachmentKeyBindings;
    }


    public TKey FindKeyByValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue value)
    {
        foreach (var entry in dictionary)
        {
            if (EqualityComparer<TValue>.Default.Equals(entry.Value, value))
            {
                return entry.Key;
            }
        }
        return default; // or throw an exception, or indicate failure some other way
    }

}
