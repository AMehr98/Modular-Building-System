using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineManager : MonoBehaviour
{
    //GameObject for creating Outline on Objects
    public GameObject OutlinedObject;
    //TextMeshGUI for changing test on UI
    public TextMeshProUGUI objNameText;
    //Call BuildingManager
    private BuildingManager buildingManager;
    //GameObject for hiding Object UI
    public GameObject ObjUI;

    // Start is called before the first frame update
    void Start()
    {   //Get BuildingManager Game Object
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //When mouse button left clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Fire Raycast from Mouse Position to Plane
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If place is hit with raycast
            if(Physics.Raycast(ray, out hit, 1000))
            {   //If object collider hit with raycast
                if (hit.collider.gameObject.CompareTag("Object"))
                {   //Outline and Select the Object
                    Select(hit.collider.gameObject);
                }
            }
        }

        //When mouse button right clicked 
        if(Input.GetMouseButtonDown(1) && OutlinedObject != null)
        {
            //Remove Outline from object and Deselect
            Deselect();
        }
    }

    //Select and Outline Object
    private void Select(GameObject obj)
    {
        if (obj == OutlinedObject) 
            return;

        if (OutlinedObject != null) 
            Deselect();

        Outline outline = obj.GetComponent<Outline>();

        if (outline == null) 
            obj.AddComponent<Outline>();
        else 
            outline.enabled = true;

        objNameText.text = obj.name;
        ObjUI.SetActive(true);
        OutlinedObject = obj;

    }

    //Move Deselect and Remove Outline from Object
    private void Deselect() 
    {
        ObjUI.SetActive(false);
        OutlinedObject.GetComponent<Outline>().enabled = false;
        OutlinedObject = null;
    }

    //Move Object
    public void Move()
    {
        buildingManager.SelectedObject = OutlinedObject;
    }

    //Delete Object
    public void Delete()
    {
        GameObject objectDelete = OutlinedObject;
        Deselect();
        Destroy(objectDelete);
    }
}

