using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrId : MonoBehaviour
{
    public int terrId = 3;
    
    public Transform intruder1;
    private void OnTriggerEnter(Collider other)
    {

        intruder1 = null;
        int cClan = other.GetComponent<SlimeAi>().CLAN;
        if (terrId != 3)
        {
            if (cClan != terrId)
            {
                
                intruder1 = other.transform;
                List<Transform> owners = new List<Transform>();
                foreach (GameObject l in GameObject.FindGameObjectsWithTag("Slime"))
                {
                    owners.Add(l.transform);
                }
                foreach (Transform b in owners)
                {
                    if (b.GetComponent<SlimeAi>().CLAN != terrId)
                    {
                        owners.Remove(b);
                        continue;
                    }
                    b.GetComponent<SlimeAi>().intruder = intruder1;
                    b.GetComponent<SlimeAi>().currentState = b.GetComponent<SlimeAi>().Aggro;

                }
            }
            else
            {
                intruder1 = null;
                

            }
        }
        Color color;
        if (terrId == 1)
        {
            color = Color.red;
        }
        else if (terrId == 2)
        {
            color = Color.blue;
        }
        else
        {
            color = Color.gray;
        }
        GetComponent<MeshRenderer>().material.color = color;

    }
        private void OnDrawGizmos()
        {


            Color color;
            if (terrId == 1)
            {
                color = Color.red;
            }
            else if (terrId == 2)
            {
                color = Color.blue;
            }
            else
            {
                color = Color.gray;
            }
            color.a /= 2;
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, new Vector3(4.107726f, 4.107726f, 4.107726f));

        }

}
