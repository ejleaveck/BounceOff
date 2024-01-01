using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pilot
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
    /// Stores the button KeyCode with Attachment definition for use during play
    /// </summary>
    public Dictionary<KeyCode, Attachment> AttachmentKeyBindings { get; private set; }

    /// <summary>
    /// Default Settings
    /// </summary>
    /// <param name="name"></param>
    /// <param name="backStory"></param>
    public Pilot(string name, string backStory)
    {
        Name = name;
        BackStory = backStory;
        PilotColorScheme = ColorScheme.RusticBlue;
        Level = 1;
        ExperiencePoints = 0;


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

   
    public void UpdateAttachmentKeyBindings(Dictionary<KeyCode, Attachment> attachmentKeyBindings)
    {
        AttachmentKeyBindings = attachmentKeyBindings;
    }

}
