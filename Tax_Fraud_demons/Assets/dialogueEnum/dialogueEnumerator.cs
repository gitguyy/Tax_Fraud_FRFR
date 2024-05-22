using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class dialogueEnumerator : MonoBehaviour
{
    public virtual void onEnter()
    {

    }
    public virtual void onEnter( DialogueSystem.Actor actor)
    {

    }
    public virtual void talkUpdate(ref string s)
    {

    }

    public virtual void onExit()
    {

    }
}
