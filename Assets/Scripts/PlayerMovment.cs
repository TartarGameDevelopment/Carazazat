using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerMovment : MonoBehaviour
{
    public GameObject Player;
    private PathNode selectedCell;
    private AvalibleCellVisual avaliableCells;


    private void Start()
    {
        avaliableCells = GetComponent<AvalibleCellVisual>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedCell = Pathfinding.Instance.GetGrid().GetGridObject(UtilsClass.GetMouseWorldPosition());
            if (selectedCell.GetPlayer() != null && selectedCell.GetPlayer().GetComponent<Player_controller>().movmentPoints > 0)
            {
                Player = selectedCell.GetPlayer();
                FindArea();

            }
            else
            {
                if (Player != null)
                {
                    Player.GetComponent<Player_controller>().SetTarget(UtilsClass.GetMouseWorldPosition());
                    if (Player.GetComponent<Player_controller>().movmentPoints <= 0)
                    {
                        Player = null;
                        Pathfinding.Instance.ResestPath();
                        avaliableCells.SetGrid(Pathfinding.Instance.GetGrid());
                    }
                    else
                    {
                        FindArea();
                    }
                    
                }
            }

        }
    }

    private void FindArea()
    {
        Player.GetComponent<Player_controller>().FindMovmentArea();
        avaliableCells.SetGrid(Pathfinding.Instance.GetGrid());
    }

}
