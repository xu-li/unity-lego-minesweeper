using System.Collections;
using System.Collections.Generic;
using LEGOModelImporter;
using Unity.LEGO.Behaviours;
using Unity.LEGO.Behaviours.Triggers;
using UnityEngine;

public class MineColumn : MonoBehaviour
{
    private int _row;
    private int _column;
    private MineSweeperGame _game;

    public int Row
    {
        get => _row;
        set => _row = value;
    }

    public int Column
    {
        get => _column;
        set => _column = value;
    }

    public MineSweeperGame Game
    {
        get => _game;
        set => _game = value;
    }

    public void Check(CheckMineAction action, Brick brick)
    {
        if (_game.Map[_row, _column] == -1)
        {
            BrickExploder.ExplodeConnectedBricks(brick);
        }
        else
        {
            // InputTrigger trigger = GetComponentInChildren<InputTrigger>();
            // trigger.sho
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInput()
    {
        Debug.Log($"[Xu]OnInput.");
    }
}
