using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public List<GameObject> TeleportPoints;
    public List<GameObject> objects;
    public bool inList;
    public enum type {off,Random,Near,Far}
    public type getType = type.Random;
    
    void Start()
    {
        for (int i = 0; i < TeleportPoints.Count; i++)
        {
            if (TeleportPoints[i] == gameObject || TeleportPoints[i] == null)
            {
                TeleportPoints.RemoveAt(i);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == true)
        {
            if (TeleportPoints.Count > 0)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] == other.gameObject)
                    {
                        inList = true;
                    }
                }

                if (inList == false && getType == type.Random)
                {
                    print(5152);
                    int random = 0;
                    random = Random.Range(0, TeleportPoints.Count);
                    other.transform.position = TeleportPoints[random].transform.position - (transform.position - other.transform.position);
                    TeleportPoints[random].GetComponent<Teleport>().objects.Remove(other.gameObject);
                    TeleportPoints[random].GetComponent<Teleport>().objects.Add(other.gameObject);
                }

                if (inList == false && getType == type.Near)
                {
                    int near = 0;
                    float d = 0;

                    for (int i = 0; i < TeleportPoints.Count; i++)
                    {

                        if (i == 0)
                        {
                            d = Vector3.Distance(TeleportPoints[0].transform.position, transform.position);
                        }

                        if (Vector3.Distance(TeleportPoints[i].transform.position, transform.position) < d)
                        {
                            d = Vector3.Distance(TeleportPoints[i].transform.position, transform.position);
                            near = i;
                        }
                    }

                    other.transform.position = TeleportPoints[near].transform.position - (transform.position - other.transform.position);
                    TeleportPoints[near].GetComponent<Teleport>().objects.Remove(other.gameObject);
                    TeleportPoints[near].GetComponent<Teleport>().objects.Add(other.gameObject);
                }

                if (inList == false && getType == type.Far)
                {
                    int far = 0;
                    float d = 0;
                   
                    for (int i = 0; i < TeleportPoints.Count; i++)
                    {
                        if (i == 0)
                        {
                            d = Vector3.Distance(TeleportPoints[0].transform.position, transform.position);
                        }

                        if (Vector3.Distance(TeleportPoints[i].transform.position, transform.position) > d)
                        {
                            d = Vector3.Distance(TeleportPoints[i].transform.position, transform.position);
                            far = i;
                        }
                    }

                    other.transform.position = TeleportPoints[far].transform.position - (transform.position - other.transform.position);
                    TeleportPoints[far].GetComponent<Teleport>().objects.Remove(other.gameObject);
                    TeleportPoints[far].GetComponent<Teleport>().objects.Add(other.gameObject);
                }
                inList = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == true)
        {
            objects.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
