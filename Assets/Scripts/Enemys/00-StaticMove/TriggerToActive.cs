using UnityEngine;
using System.Collections;

public class TriggerToActive : MonoBehaviour {

    public GameObject myController;


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ship"))
        {
            myController.GetComponent<StaticMoveBehavior>().SetActive(true, other.gameObject);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ship"))
        {
            myController.GetComponent<StaticMoveBehavior>().SetActive(false,other.gameObject);
        }
    }
	
}
