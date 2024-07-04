using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField]
    ProgressBaseObjects obj;
    progressionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        manager = progressionManager.Instance;
        if (obj.changeAt <= manager.progressionLevel)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
