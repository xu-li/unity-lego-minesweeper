using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.LEGO.Behaviours.Triggers;
using Unity.LEGO.Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineSweeperGame : MonoBehaviour
{
    [SerializeField]
    public int rows = 5;
    
    [SerializeField]
    public int cols = 5;

    [SerializeField]
    public int totalMines = 3;

    [SerializeField]
    public float spacing = 0.5f;
    
    [SerializeField] public GameObject bombPrefab;
    [SerializeField] public GameObject numberPrefab;
    
    [SerializeField] public Transform cellsContainer;
    [SerializeField] public Variable remainingVariable;
    [SerializeField] public CounterTrigger counterTrigger;


    private int[,] _map;
    
    public static MineSweeperGame Singleton;

    public int[,] Map
    {
        get => _map;
        set => _map = value;
    }

    public void GenerateMap()
    {
        GenerateMapData();
        
        // Destroy old cells
        foreach (Transform oldCell in cellsContainer)
        {
            Destroy(oldCell.gameObject);
        }

        Vector3 containerPosition = cellsContainer.position;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int num = _map[i, j];
                GameObject prefab = num == -1 ? bombPrefab : numberPrefab;
                GameObject go = Instantiate(
                    prefab, 
                    new Vector3(i * spacing + containerPosition.x, 0, j * spacing +  + containerPosition.z), 
                    Quaternion.identity, 
                    cellsContainer);

                if (num >= 0)
                {
                    go.GetComponent<NumberedColumn>().text.text = num.ToString();
                }
            }
        }
    }

    void GenerateMapData()
    {
        _map = new int[rows, cols];

        List<int> minePositions = new List<int>(totalMines);
        do
        {
            int pos = Random.Range(0, rows * cols);
            if (!minePositions.Contains(pos))
            {
                minePositions.Add(pos);
                _map[pos / cols, pos % cols] = -1;
            }
        } while (minePositions.Count != totalMines);

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                // It's a mine
                if (_map[i, j] == -1)
                {
                    for (int x = i == 0 ? 0 : i - 1; x <= (i == rows - 1 ? rows - 1 : i + 1); x++)
                    {
                        for (int y = j == 0 ? 0 : j - 1; y <= (j == cols - 1 ? cols - 1 : j + 1); y++)
                        {
                            if (_map[x, y] != -1 && !(x == i && y == j))
                            {
                                _map[x, y]++;
                            }
                        }
                    }
                }
            }
        }
        

        // FieldInfo fieldInfo = counterTrigger.GetType().GetField("m_Conditions",
        //     BindingFlags.NonPublic | BindingFlags.SetField | BindingFlags.Instance);
        // List<Condition> conditions = (List<Condition>) fieldInfo.GetValue(counterTrigger);
        // if (conditions.Count > 0)
        // {
        //     conditions[0].Value = rows * cols - totalMines;
        //     fieldInfo.SetValue(counterTrigger, conditions);
        // }
    }

    public void DebugMap()
    {
        string message = "";
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                message += _map[i, j] + " ";
            }

            message += Environment.NewLine;
        }
        Debug.Log(message);
    }

    void OnGameOver(GameOverEvent gameOverEvent)
    {
        // GenerateMap();
        // DebugMap();
    }

    void OnVariableAdded(VariableAdded variableAddedEvent)
    {
        if (variableAddedEvent.Variable.Name.Equals("Remaining"))
        {
            VariableManager.SetValue(remainingVariable, rows * cols - totalMines);
        }
    }

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }

        EventManager.AddListener<VariableAdded>(OnVariableAdded);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        DebugMap();
    }
    
    void OnDestroy()
    {
        EventManager.RemoveListener<VariableAdded>(OnVariableAdded);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }
}
