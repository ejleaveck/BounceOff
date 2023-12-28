using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Attachment
{
    public override string AttachmentName => "Shield";
    [SerializeField] private float shieldBurnRate;
    public override float BurnRate => shieldBurnRate;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        spriteRenderer.enabled = true;
    }

    public override void Deactivate()
    {
       spriteRenderer.enabled = false;
    }
}
   
