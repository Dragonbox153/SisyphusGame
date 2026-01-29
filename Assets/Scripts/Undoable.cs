using System;
using System.Collections.Generic;
using UnityEngine;

public class Undoable : MonoBehaviour
{
    public List<Vector2Int> previousPositions = new List<Vector2Int>();
    
   [SerializeField] GameGrid grid;
   [SerializeField] private PlayerController toObserve;

    private void Awake()
    {
        if (toObserve != null)
        {
            toObserve.Undo += UndoMove;
            toObserve.UpdatePosition += DoMove;
        }
    }

    public void DoMove()
    {
        previousPositions.Add(Vector2Int.RoundToInt(transform.position));
    }

    public void UndoMove()
    {
        if(previousPositions.Count > 0)
        {
            if ((Vector2)transform.position != (Vector2)(previousPositions[previousPositions.Count - 1]))
            {
                grid.MovePosition(Vector2Int.RoundToInt(transform.position), previousPositions[previousPositions.Count - 1] - Vector2Int.RoundToInt(transform.position));
                transform.position = (Vector3Int)(previousPositions[previousPositions.Count - 1]);
            }
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }
}
