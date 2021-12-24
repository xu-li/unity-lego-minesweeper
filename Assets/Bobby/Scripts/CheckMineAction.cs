using System.Collections.Generic;
using Unity.LEGO.Behaviours;
using Unity.LEGO.Behaviours.Actions;
using UnityEngine;

public class CheckMineAction : Action
{
    bool _checked = false;

    private MineSweeperGame _game;
    
    
    protected override void Reset()
    {
        base.Reset();

        m_IconPath = "Assets/LEGO/Gizmos/LEGO Behaviour Icons/Random Trigger.png";
    }

    protected override void Start()
    {
        base.Start();

        _game = MineSweeperGame.Singleton;
    }

    protected void Update()
    {
        if (m_Active)
        {
            if (!_checked)
            {
                MineColumn column = GetComponentInParent<MineColumn>();
                column.Check(this, m_Brick);
                _checked = true;
            }
        }
    }
    
}