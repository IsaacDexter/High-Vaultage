using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_bigDoor  : s_triggerable
{
    public Animator anim;
    public Transform door;
    // Start is called before the first frame update

    override public void Deactivate()
    {
        base.Deactivate();
        anim.SetBool("open", false);
    }
    override public void Activate()
    {
        base.Activate();
        anim.SetBool("open", true);
    }
}
