using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class Player_controller : MonoBehaviour
{
    public float speed = 10f;

    private bool moving = false;
    public int movmentPoints = 3;
    private int targetPointCount = 0;
    private PathNode currentNode;


    List<Vector3> path;
    void Start()
    {
        
    }


    public void FindMovmentArea()
    {
        Pathfinding.Instance.FindArea(Pathfinding.Instance.GetGrid().GetWorldPosition(currentNode.GetX(), currentNode.GetY()), movmentPoints);
    }


    private Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTarget(Vector3 targetWorldPosition)
    {
        
        path = null;
        if (!moving)
        {
            path = Pathfinding.Instance.FindPath(targetWorldPosition);
            if (path != null)
            {
                targetPointCount = 0;
                movmentPoints -= Pathfinding.Instance.GetGrid().GetGridObject(targetWorldPosition).gCost;
                Pathfinding.Instance.GetGrid().GetGridObject(GetPosition()).SetPlayer(null);
                Pathfinding.Instance.GetGrid().GetGridObject(targetWorldPosition).SetPlayer(this.gameObject);
                moving = true;
                ResetPosition();
                SetPosition();
            }
            else
            {
                Pathfinding.Instance.ResestPath();
                Debug.Log("inactive");
            }
        }
    }

    private void ResetPosition()
    {
        if (currentNode != null)
            currentNode.SetPlayer(null);
    }

    private void SetPosition()
    {
        Pathfinding.Instance.GetGrid().GetGridObject(path[path.Count - 1]).SetPlayer(this.gameObject);
        currentNode = Pathfinding.Instance.GetGrid().GetGridObject(path[path.Count-1]);
        
    }

    void Update()
    {

        if (currentNode == null)
        {
            if (Pathfinding.Instance.GetGrid().GetGridObject(GetPosition()) != null)
            {
                Pathfinding.Instance.GetGrid().GetGridObject(GetPosition()).SetPlayer(this.gameObject);
                currentNode = Pathfinding.Instance.GetGrid().GetGridObject(GetPosition());
                transform.position = Pathfinding.Instance.GetGrid().GetWorldPosition(currentNode.GetX(), currentNode.GetY()) + new Vector3(Pathfinding.Instance.GetGrid().GetCellSize(), Pathfinding.Instance.GetGrid().GetCellSize())/2;
            }
        }
        if (moving)
        {
            if (targetPointCount < path.Count)
            {
                if (Vector3.Distance(GetPosition(), path[targetPointCount]) > 0.1f)
                {
                    Vector3 moveDir = (path[targetPointCount] - GetPosition()).normalized;
                    transform.position += moveDir * Time.deltaTime * speed;
                }
                else
                {
                    
                    targetPointCount++;
                    Debug.Log(movmentPoints);
                }
            }
            else
            {
                moving = false;
                if(movmentPoints > 0)
                {
                    FindMovmentArea();
                }
                else
                {
                    Pathfinding.Instance.ResestPath();
                }
            }
        }

    }
}
