using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class controlstop : MonoBehaviour
{
    // Start is called before the first frame update
     public PlayableDirector timeline;
    void Start()
    {
        timeline.time = 0;
        timeline.Stop();
        //timeline.Evaluate();
        //timeline = GetComponent<PlayDirector>();
        // timeline.time = 0;
        // timeline.Stop();
        // timeline.Evaluate();
    }
    int t = 0;
    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(timeline.time);
        
        t++;
        Debug.Log("t = "+t);
        if(t == 200)
            timeline.Play();
    }
}
