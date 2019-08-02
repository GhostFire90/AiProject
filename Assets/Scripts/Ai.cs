using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ai : MonoBehaviour
{
    public delegate void CurrentState();
    public CurrentState currentState;
    GameObject Population;
    GameObject BluePop;
    GameObject RedPop;
    
    int ri = 2;
    int bi = 2;
    List<GameObject> Red = new List<GameObject>();
    List<GameObject> Blu = new List<GameObject>();
    float ctimerDelay = 5;
    float ctimerTime = 0;
    

    protected List<Transform> foodLoc = new List<Transform>();
    // Update is called once per frame
    
    void Update()
    {
        Population = GameObject.FindGameObjectWithTag("Pop");
        BluePop = GameObject.Find("BluePop");
        RedPop = GameObject.Find("RedPop");
        
        Text r = RedPop.GetComponent<Text>();
        Text b = BluePop.GetComponent<Text>();
        Text t = Population.GetComponent<Text>();
        t.text = GameObject.FindGameObjectsWithTag("Slime").Length.ToString();
        
        
        if (currentState != null)
        {
            currentState();
            
        }
        if(GameObject.FindGameObjectsWithTag("Slime").Length > 100)
        {
            SlimeAi slimeAI = GameObject.FindGameObjectWithTag("Slime").gameObject.GetComponent<SlimeAi>();
            if(slimeAI != null)
            {
                slimeAI.currentState = slimeAI.Death;
            }

        }
        foreach(GameObject l in GameObject.FindGameObjectsWithTag("Slime"))
        {                      
            
            if (l.GetComponent<SlimeAi>().CLAN == 1)
            {
                ri++;
                Red.Add(l);
                
                r.text = Red.ToArray().Length.ToString();
            }
            else if (l.GetComponent<SlimeAi>().CLAN == 2)
            {
                bi++;
                
                Blu.Add(l);
                b.text = Blu.ToArray().Length.ToString();
            }
            

        }
        ri = 0;
        bi = 0;
        Blu.Clear();
        Red.Clear();

        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("Main");
        }
        
        



    }
    
}
