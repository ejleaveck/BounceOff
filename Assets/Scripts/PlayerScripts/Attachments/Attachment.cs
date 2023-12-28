using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attachment : MonoBehaviour
{
    public abstract string AttachmentName { get; }
    public abstract float BurnRate { get; }

    public abstract void Activate();

    public abstract void Deactivate();
}
