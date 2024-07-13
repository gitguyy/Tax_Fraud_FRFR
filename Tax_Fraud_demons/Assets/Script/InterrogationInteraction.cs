using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterrogationInteraction : MonoBehaviour
{
    [Serializable]
    struct ActorSounds
    {
        public List<AudioClip> sounds;
    }
    #region Variables
    InterrogationLogic system;
    
    talkingBehavior t;
    string text = "";
    string originalText = "";
    [SerializeField]
    bool doneSpelling = true;
    ShowDialogue s;
    [SerializeField]
    public SuspectAnimator anim;
    AudioSource source;
    [SerializeField]
    ActorSounds[] sounds;
    [SerializeField]
    ActorSounds mitch;
    int prevSoundMitch;
    int prevSoundNPC;
    
    
    
    #endregion
    // Start is called before the first frame update
    
    void Start()
    {
        t= gameObject.GetComponent<talkingBehavior>();
        doneSpelling = true;
        system = InterrogationLogic.Instance;
        s = FindAnyObjectByType<ShowDialogue>();
        source = gameObject.GetComponent<AudioSource>();
      
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (!doneSpelling && !system.isAngry)
        {
           

            t.spellLineUpdate(ref text, ref doneSpelling, originalText);
            s.setText(text);
            PlaySound();
            
            if (doneSpelling)
            {
                
                system.NextDialogue();

            }
        }

        if(!doneSpelling && system.isAngry)
        {
            t.spellLineUpdate(ref text, ref doneSpelling, originalText);
            s.setText(text);
            PlaySound();
            if (doneSpelling)
            {
              
                system.isAngry = false;
               

            }

        }


    }

    public void spellNextText()
    {
       
        if (doneSpelling)
        {
            originalText = system.getText();
            anim.SetTrigger();
            
            doneSpelling = false;
           
            //spellNextText();
        }
        
    }

    

    void spellPrevioustext()
    {

    }

    void PlaySound()
    {
        if (system.returnState() != InterrogationLogic.suspectState.Null)
        {
            ActorSounds curSounds = sounds[system.suspectID];
            if (!source.isPlaying!)
            {
                int randomSound = UnityEngine.Random.Range(0, sounds[system.suspectID].sounds.Count - 1);
                if (randomSound == prevSoundNPC)
                {
                    if (randomSound > 0)
                    {
                        randomSound--;
                    }
                    else if (randomSound < curSounds.sounds.Count - 1)
                    {
                        randomSound++;
                    }
                }
                if (randomSound >= curSounds.sounds.Count)
                {
                    randomSound = 0;
                }
                source.clip = curSounds.sounds[randomSound];
                source.PlayOneShot(curSounds.sounds[randomSound]);
                //Debug.Log("sound played");
                prevSoundNPC = randomSound;

            }
        }
        else
        {
            if (!source.isPlaying!)
            {
                int randomSound = UnityEngine.Random.Range(0, mitch.sounds.Count - 1);
                if (randomSound == prevSoundMitch)
                {

                    if (randomSound > 0)
                    {
                        randomSound--;

                    }
                    else if (randomSound < mitch.sounds.Count - 1)
                    {
                        randomSound++;

                    }
                    else
                    {
                        randomSound = UnityEngine.Random.Range(0, mitch.sounds.Count - 1);

                    }

                }

                source.clip = mitch.sounds[randomSound];
                source.PlayOneShot(mitch.sounds[randomSound]);
                prevSoundMitch = randomSound;

            }
        }
    }
    
}
