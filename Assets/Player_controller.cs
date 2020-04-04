using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Player_controller : MonoBehaviour
{
    public float speed = 10f;

    private bool moving = false;
    private int targetPointCount = 0;

    List<Vector3> path;
    void Start()
    {
        path = new List<Vector3>();
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
                moving = true;
            }
        }
    }
    void Update()
    {
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
                }
            }
            else
            {
                moving = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetTarget(UtilsClass.GetMouseWorldPosition());
            }
        }

    }
}
