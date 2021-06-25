using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class EditSnap : MonoBehaviour
{

  
    [SerializeField] TextMesh textMesh;

    Vector3 SnapPos;
    Waypoint waypoint;
    // Start is called before the first frame update
    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
       
        transform.position = new Vector3(waypoint.GetGridPos().x * gridSize,0, waypoint.GetGridPos().y*gridSize);
    }

    void UpdateLabel() {
        textMesh = GetComponentInChildren<TextMesh>();
        int gridSize = waypoint.GetGridSize();
        string labelText = waypoint.GetGridPos().x  + "," + waypoint.GetGridPos().y;
        //textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
