using System;
using System.ComponentModel;
using UnityEngine;

public class AttachmentsController : MonoBehaviour
{

    public enum Attachment
    {
        [Description("Tractor Beam")]
        TractorBeam,
        [Description("Shield")]
        Shield,
        [Description("Laser Blaster")]
        Laser,
        [Description("Gravity Well Diffusor")]
        AntiGravity
    }

    private void Start()
    {
        ChangeAttachment(Attachment.TractorBeam);
    }

    /// <summary>
    /// Future use if I want to build in more descriptive strings of the enums for displaying to the UI.
    /// Enum Param needs to have [Description("string")] above each enum value in the variable declaration.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static event Action<Attachment> AttachmentChanged;

    private void ChangeAttachment(Attachment newAttachment)
    {
        AttachmentChanged?.Invoke(newAttachment);
    }
}
