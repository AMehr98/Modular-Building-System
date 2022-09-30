using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    //Call Building Manager Class
    BuildingManager buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        //Get BuildingManager Game Object
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    //When Object Collider Triggered
    private void OnTriggerEnter(Collider other)
    {
        //Check tag of Objects
        if (other.gameObject.CompareTag("Object"))
        {
            //If Colliding Set PlaceAllow to false
            buildingManager.PlaceAllow = false;
        }
    }

    //When Leaving Object Collider Trigger
    private void OnTriggerExit(Collider other)
    {   
        //Check tag of Objects
        if (other.gameObject.CompareTag("Object"))
        {
            //If NOT Colliding Set PlaceAllow to true
            buildingManager.PlaceAllow = true;
        }
    }
}
