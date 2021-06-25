using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    public List<Waypoint> path;

    public List<Waypoint> GetPath() {
        if (path.Count == 0) //no fue calculado
        {
            CalculatePath();

        }
        return path;
        
    }

    private void CalculatePath()
    {
        LoadBlocks();
        //ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint); //dice que el end point no puede serr usado y lo agrega a la lista
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) {
            path.Add(previous);
            previous.isPlaceable = false;
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        startWaypoint.isPlaceable = false;
        path.Reverse();
        //print(path[0]);
    }

    void SetAsPath(Waypoint waypoint) {
        path.Add(endWaypoint);
        endWaypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning) {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint) {
            print("Es igual wuachin");
            isRunning = false;
        }
    }
    //explora los bloques de a uno, sumandole 1 o -1
    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions) {
            
            Vector2Int explorationCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(explorationCoordinates))
            {
                QueueNewNeighbours(explorationCoordinates);
            }
        }
    }

    //verifica si hay algun bloque al lado, y si esta revisado ya
    private void QueueNewNeighbours(Vector2Int explorationCoordinates)
    {
        Waypoint neighbout = grid[explorationCoordinates];
        if (neighbout.isExplored || queue.Contains(neighbout)) // funciona tan bien por la condicion si se exploro, 
        {
            //nada
        }
        else
        {
            
            queue.Enqueue(neighbout);
            neighbout.exploredFrom = searchCenter;
        }
        
    }

   
    //busca un bloque y si no lo tiene lo agrega al diccionario
    void LoadBlocks() {
        var waypoints = FindObjectsOfType<Waypoint>(); // es como un array
        foreach (Waypoint waypoint in waypoints) {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                //Debug.LogError("EstaError "+ waypoint.GetGridPos());
            }
            else {

                grid.Add(waypoint.GetGridPos(), waypoint);
                //waypoint.SetTopColor(Color.black);
            }
            
        }
    }
}
