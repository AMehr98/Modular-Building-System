using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    //Array to store game Objects
    public GameObject[] objects;
    public GameObject SelectedObject;

    //Vector and Raycast top obtain Object Position
    private Vector3 position;
    private RaycastHit hit;

    //Variable for how much the Object rotates
    public float RotateAmount;

    //Layer Mask for the ground
    [SerializeField] private LayerMask layerMask;

    //Variable for Gridsize, Lock and Toggle
    public float GridSize;
    bool GridLock;
    [SerializeField] private Toggle GridToggle;

    //Toggle to allow placement of Objects is not Colliding
    public bool PlaceAllow;

    //Create Material Array 
    [SerializeField] private Material[] materials;

    // Update is called once per frame
    void Update()
    {
        //If an Object is selected 
        if (SelectedObject != null)
        {   
            //Update Material of Object
            UpdateMat();

            //If Grid Lock is On
            if (GridLock)
            {
                SelectedObject.transform.position = new Vector3(
                    RoundToNearestGridLine(position.x),
                    RoundToNearestGridLine(position.y),
                    RoundToNearestGridLine(position.z));
            }
            //Grid Lock is Off
            else
            {
                //Move Object to position varaible
                SelectedObject.transform.position = position;
            }
            //Place with mouse left click
            if (Input.GetMouseButton(0) && PlaceAllow)
            {
                PlaceObject(); 
            }
            
            //Rotate Selected Object when R key
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            
        }
    }

    //Update is called at the end of each frame
    private void FixedUpdate()
    {
        //Sends Ray from mouse position to enviroment plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Obtains position of raycast on the enviroment plane
        if(Physics.Raycast(ray,out hit, 1000, layerMask))
        {
            position = hit.point;
        }
    }

    //Changes Material of Object to indicate if placement is allowed
    public void UpdateMat()
    {
        if (PlaceAllow)
        {
            SelectedObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        if (!PlaceAllow)
        {
            SelectedObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    //Instantiate the Object and move to position varaible
    public void SelectObject(int index)
    {
        SelectedObject = Instantiate(objects[index], position, transform.rotation);
        SelectedObject.name = objects[index].name;
    }

 
    //Rotate Object by value of RotateAmount
    public void RotateObject()
    {
        SelectedObject.transform.Rotate(Vector3.up, RotateAmount);
    }

    //Toggle for Grid Lock
    public void ToggleGridLock()
    {
        if (GridToggle.isOn)
        {
            GridLock = true; 
        }
        else
        {
            GridLock = false;
        }
    }

    //Round position to nearest grid value.
    float RoundToNearestGridLine(float position)
    {
        float Diff;
        Diff  = position % GridSize;
        position -= Diff;

        if(Diff > (GridSize/2))
        {
            position += GridSize;
        }
        return position;
    }


    //Remove Instantiated Object when Object Placed
    public void PlaceObject()
    {
        SelectedObject.GetComponent<MeshRenderer>().material = materials[2];
        SelectedObject = null;
    }

}
