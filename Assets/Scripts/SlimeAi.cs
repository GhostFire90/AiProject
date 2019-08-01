using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAi : Ai 
{
    Rigidbody rb;
    public float jumpDelay = 1f;
    public float jumpHeight;
    public float jumpRange = 20f;
    float jumpTime = 0;
    RaycastHit hit;
    float hungeryTime = 0;
    public float hungeryDelay;
    bool hungry;
    Transform food;
    float eatingTime = 0;
    public float eatingDelay;
    public Object slimeP;
    float deathRange_start = 10f;
    public float deathRange_end;
    float deathDelay;
    float deathTime = 0;
    float strength = 5;
    public int CLAN;
    public float starveDelay;
    float starveTime = 0;
    public Transform intruder;
    bool Destroyed;
    void Idle()
    {
        //Transform intruder = 
        deathTime += Time.deltaTime;
        Vector3 angle = new Vector3(Random.Range(-jumpRange, jumpRange), 0 , Random.Range(-jumpRange, jumpRange));
        if (OnGround())
        {
            jumpTime += Time.deltaTime;
            if (jumpTime >= jumpDelay)
            {
                jumpTime -= jumpDelay;
                Jump(angle);
            }
                
        }
        hungeryTime += Time.deltaTime;
        
        if (hungeryTime >= hungeryDelay)
        {
            hungeryTime -= hungeryDelay;
            currentState = Hungry;
            Debug.Log("Hungry");
            
            
        }
        else if(transform.localScale.x >= 101 && GameObject.FindGameObjectsWithTag("Slime").Length <= 250)
        {
            currentState = Split;
            Debug.Log("Split");
        }
        else if (transform.localScale.x >= 101 && GameObject.FindGameObjectsWithTag("Slime").Length > 250)
        {
            currentState = Max;
            Debug.Log("Max");
        }

        else if (deathTime >= deathDelay)
        {
            currentState = Death;
            Debug.Log("Death");
        }
        else if(intruder != null && intruder.gameObject.GetComponent<SlimeAi>().CLAN != CLAN)
        {
            currentState = Aggro;
            Debug.Log("Aggro");
            
        }
        else
        {
            currentState = Idle;
            
        }
    }
    void Split()
    {
        
            transform.localScale -= new Vector3(1.2f, 1.2f, 1.2f);
            Instantiate(slimeP);
            currentState = Idle;    
    }
    void Max()
    {
        transform.localScale -= new Vector3(1.2f, 1.2f, 1.2f);
        currentState = Idle;
    }
    public void Aggro()
    {
        if (OnGround())
        {

            if (intruder != null)
            {
                if (jumpTime >= jumpDelay)
                {
                    Vector3 d = transform.position - intruder.position;
                    float r = Mathf.Atan2(d.z, -d.x);
                    Vector3 angle = Quaternion.Euler(0, r * Mathf.Rad2Deg, 0) * new Vector3(0, 0, -jumpRange);
                    jumpTime -= jumpDelay;
                    Jump(angle);
                }
            }
        }

        else if (intruder == null)
        {
                
            currentState = Idle;
                
        }
        
    }
    void Hungry()
    {
        foreach (GameObject l in GameObject.FindGameObjectsWithTag("Food"))
        {
            foodLoc.Add(l.transform);
        }
        Transform a = null;
        foreach (Transform b in foodLoc)
        {
            
            if (a != null)
            {
                if (Vector3.Distance(b.position, transform.position) < Vector3.Distance(a.position, transform.position))
                {
                    a = b;
                }
                
            }
            else
            {
                a = b;
            }
        }
        food = a;
        jumpTime += Time.deltaTime;
        if (OnGround())
        {
            if (jumpTime >= jumpDelay)
            {
                Vector3 d = transform.position - food.position;
                float r = Mathf.Atan2(d.z, -d.x);
                Vector3 angle = Quaternion.Euler(0, r * Mathf.Rad2Deg, 0) * new Vector3(0, 0, -jumpRange);
                jumpTime -= jumpDelay;
                Jump(angle);
            }
        }
        starveTime += Time.deltaTime;
        if (starveTime >= starveDelay)
        {
            currentState = Death;
        }
        if(Physics.Raycast(new Ray(transform.position, food.position - transform.position), out hit, 3))
        {
            eatingTime += Time.deltaTime;
            if (eatingTime >= eatingDelay)
            {
                transform.localScale += new Vector3(.3f, .3f, .3f);
                currentState = Idle;
                if(food.GetComponent<TerrId>().terrId != CLAN)
                {
                    food.GetComponent<TerrId>().terrId = CLAN;
                }
            }
        }
    }
    public void Death()
    {


        if (Destroyed != true)
        {
            transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);

            if (transform.localScale.x <= 0)

            {
                Destroy(gameObject);
            }
        }
        
        
        
    }
    void Jump(Vector3 target)
    {
        rb.velocity =
            Quaternion.Euler(target)
            * Vector3.up
            * jumpHeight;
        transform.LookAt(transform.position + target);
    }

    bool OnGround()
    {
        if(Physics.Raycast(new Ray(transform.position, Vector3.down),out hit, 2))
        {
            
            return true;
        }
        return false;
    }
    void Start()
    {
        
        int i = Random.Range(0, 10);
        rb = GetComponent<Rigidbody>();
        food = GameObject.FindGameObjectWithTag("Food").transform;
        deathDelay = Random.Range(deathRange_start, deathRange_end);
        
        if (i <= 1)
            strength = strength - Random.Range(-10, 10);
        
        currentState = Idle;
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(food)
        {
            Gizmos.DrawRay(transform.position, Vector3.down);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        List<GameObject> CantFight = new List<GameObject>();
        SlimeAi slimeAI = collision.gameObject.GetComponent<SlimeAi>();
        if (slimeAI != null && CLAN != slimeAI.CLAN)
        {
            if (!CantFight.Contains(collision.gameObject))
            {
                if (collision.gameObject.GetComponent<SlimeAi>().strength > strength && collision.gameObject.tag == "Slime")
                {
                    currentState = Death;
                }
                else if (collision.gameObject.GetComponent<SlimeAi>().strength < strength && collision.gameObject.tag == "Slime")
                {
                    collision.gameObject.GetComponent<SlimeAi>().currentState = Death;
                }
                else if (strength == collision.gameObject.GetComponent<SlimeAi>().strength)
                {
                    CantFight.Add(collision.gameObject);
                }
            }
        }


    }
    private void OnDestroy()
    {
        Destroyed = true;
    }
}
