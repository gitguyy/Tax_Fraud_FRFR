using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectAnimator : MonoBehaviour
{
    private Animator animator;
    public RuntimeAnimatorController controller;
    private InterrogationLogic data;
    private MySceneManager sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        
        sceneManager = MySceneManager.Instance;
        data = InterrogationLogic.Instance;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetTrigger()
    {
        if (data.returnState() == InterrogationLogic.suspectState.Null) { animator.SetTrigger("Idle"); Debug.Log("Idle"); return; }
        if (data.returnState() == InterrogationLogic.suspectState.Talking) { animator.SetTrigger("talk"); Debug.Log("talking"); return; }
        if (data.returnState() == InterrogationLogic.suspectState.Contemplating) { animator.SetTrigger("ponder"); Debug.Log("pondering"); return; }
       
        if (data.returnState() == InterrogationLogic.suspectState.Nervous) { animator.SetTrigger("anxious"); Debug.Log("anxious"); return; }
        if (data.returnState() == InterrogationLogic.suspectState.Anxious) { animator.SetTrigger("stressed"); Debug.Log("nervous"); return; }
    }

    public void LoadAnimator()
    {
        controller = data.info.sprites;
        animator.runtimeAnimatorController = controller;
    }


}
