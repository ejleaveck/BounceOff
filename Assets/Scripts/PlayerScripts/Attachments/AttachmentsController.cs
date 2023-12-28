using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentsController : MonoBehaviour
{

    public int CurrentAttachmentIndex { get; private set; }

    private Attachment[] attachments;

    public static event Action<Attachment> OnAttachmentSwitched;

    private void Start()
    {
        CurrentAttachmentIndex = 0;

        //initialize the attachments array by getting any attachments that are direct children of Attachments game object.
      var attachmentsList = new List<Attachment>();

        foreach(Transform child in transform)
        {
            var attachment = child.GetComponent<Attachment>();
            if(attachment != null )
            {
                attachmentsList.Add(attachment);
            }
        }

        attachments = attachmentsList.ToArray();

        //Deactivate all at start
        foreach(var attachment in attachments)
        {
            attachment.Deactivate();
        }

        if (attachments.Length > 0)
        {
            attachments[CurrentAttachmentIndex].Activate();
            NotifyAttachmentSwitched();
        }

    }

    public void SwitchAttachment()
    {
        attachments[CurrentAttachmentIndex].Deactivate();
        CurrentAttachmentIndex = (CurrentAttachmentIndex +1) % attachments.Length;
        attachments[CurrentAttachmentIndex].Activate();

        OnAttachmentSwitched?.Invoke(attachments[CurrentAttachmentIndex]);
    }

    private void NotifyAttachmentSwitched()
    {
        OnAttachmentSwitched?.Invoke(attachments[CurrentAttachmentIndex]);
    }

    public void UseCurrentAttachment(bool use)
    {
        if(use)
        {
            attachments[CurrentAttachmentIndex].Activate();
        }    
        else
        {
            attachments[CurrentAttachmentIndex].Deactivate();
        }
    }

}
