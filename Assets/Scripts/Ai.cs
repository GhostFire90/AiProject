using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ai : MonoBehaviour
{
    public delegate void CurrentState();
    public CurrentState currentState;
    GameObject Population;
    
    protected List<Transform> foodLoc = new List<Transform>();
    // Update is called once per frame
    
    void Update()
    {
        Population = GameObject.FindGameObjectWithTag("Pop");
        Text t = Population.GetComponent<Text>();
        t.text = GameObject.FindGameObjectsWithTag("Slime").Length.ToString();
        if (currentState != null)
        {
            currentState();
            
        }
        if(GameObject.FindGameObjectsWithTag("Slime").Length > 250)
        {
            SlimeAi slimeAI = GameObject.FindGameObjectWithTag("Slime").gameObject.GetComponent<SlimeAi>();
            if(slimeAI != null)
            {
                slimeAI.currentState = slimeAI.Death;
            }

        }
        
        

    }
    
}
