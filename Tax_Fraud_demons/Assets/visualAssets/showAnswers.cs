using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class showAnswers : MonoBehaviour
{
    //Prefab
    [SerializeField]
    string answer;
    string copy;
    talkingBehavior t;
    public bool answered = false;
    public bool gaveAnswer;
    Vector2 position;
    Transform playerTransform;
    public bool startTalking;
    TextMeshProUGUI myText;
    public GameObject bubble;
    public GameObject copyBubble;
    public int answerID;
    Camera cam;
    Vector2 initialPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myText = GetComponent<TextMeshProUGUI>();
        position = new Vector2(0,0);
        t = gameObject.AddComponent<talkingBehavior>();
        t.getActor();
        t.setTimer(0.1f);
        myText.text = "";
        copyBubble = Instantiate(bubble, cam.ScreenToWorldPoint(transform.position), Quaternion.identity);
        initialPos = transform.position;

        

        

    }

    // Update is called once per frame
    void Update()
    {
        if(startTalking && !answered)
        {
           
           
            //Debug.Log("answer: " + answer);
            t.spellLineUpdate(ref answer, ref answered, copy);
            myText.text = answer;
            if(copyBubble != null)
            {
                copyBubble.GetComponent<BubbleInformation>().updateLength(answer.Length);
            }
          
            if (answered)
            {
                startTalking = false;
            }
        }
        else if(startTalking && answered)
        {
            myText.text = copy;
        }
        if(copyBubble.GetComponent<DialogueBubble>().hovering)
        {
            copyBubble.GetComponent<SpriteRenderer>().color = Color.green;
            if(Input.GetMouseButtonDown(0))
            {
                
                
               
                EventManager g = EventManager.Instance;
                g.RaiseEvent(answerID);
                gaveAnswer = true;


            }
        }
        else
        {
            copyBubble.GetComponent<SpriteRenderer>().color = Color.white;
        }

        myText.transform.position = cam.WorldToScreenPoint(copyBubble.transform.position);
        
    }

   

    public void setPos(Vector2 pos)
    {
        position = pos;
    }

    public void setString(string s)
    {
        answer = s;
        //Debug.Log("answer: " + answer);
        copy = s;
    }

    public void setTransform(Transform t)
    {
        playerTransform = t;
    }

    public void setID(int ID)
    {
        answerID = ID;
    }

    public void setTransform(Vector3 t)
    {
     
        copyBubble.transform.position = t;
       
    }

    public void selfTerminate()
    {
        Destroy(copyBubble);
        Destroy(gameObject);
    }

    
}
