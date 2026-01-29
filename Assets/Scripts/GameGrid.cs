using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, bool> filledPositions = new Dictionary<Vector2Int, bool>();
    public GameObject[] moveables;
    public Tilemap walls;
    public Tilemap icyTiles;

    public Vector2Int worldSize;

    public static GameGrid Instance {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        FillPositions();
    }

    public void FillPositions()
    {
        foreach (var moveable in moveables)
        {
            filledPositions.Add(Vector2Int.RoundToInt(moveable.transform.position), true);
        }

        for(int x = -worldSize.x / 2; x < worldSize.x / 2; x++)
        {
            for(int y = -worldSize.y / 2; y < worldSize.y / 2; y++)
            {
                if (walls.HasTile(new Vector3Int(x, y, 0)))
                {
                    filledPositions.Add(new Vector2Int(x, y), false);
                }
            }
        }
    }

    public bool IsFree(Vector2Int position)
    {
        if(!filledPositions.ContainsKey(position))
        {
            return true;
        }
        return false;
    }

    public bool IsPushable(Vector2Int position)
    {
        if(filledPositions.ContainsKey(position) && filledPositions[position])
        {
            return true;
        }
        return false;
    }

    public GameObject GetObjectAt(Vector2Int position)
    {
        foreach(var moveable in moveables)
        {
            if(position == Vector2Int.RoundToInt(moveable.transform.position))
            {
                return moveable;
            }
        }
        return null;
    }

    public void MovePosition(Vector2Int position, Vector2Int direction)
    {
        filledPositions.Add(position + direction, true);
        filledPositions.Remove(position);
    }
}
