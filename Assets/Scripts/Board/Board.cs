using System;
using UnityEngine;
//using UnityToolbox.GizmosTools;

/// <summary>
/// Contains every and manage their spawn
/// </summary>
public class Board : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Possible yaw rotations of cell
    /// </summary>
    readonly float[] YAW_ROTATIONS_CELL = new float[]
    {
        180f,
        -90f,
        0f,
        90f
    };
    public const int BOARD_SIZE = 40;
    const int BOARD_HALFSIZE = 20;
    const int BOARD_LINE = 10;

    /// <summary>
    /// Invoke when a cell action is ended
    /// </summary>
    public event Action<Cell> OnCellTriggered = null;

    [SerializeField]
    Cell[] cells = new Cell[BOARD_SIZE];

    /// <summary>
    /// Size of the board used for spawning cell
    /// </summary>
    [SerializeField, Range(0.01f, 10f)]
    float xSize = 1f, zSize = 1f;

    /// <summary>
    /// Returns true if board has already spawns cells.
    /// </summary>
    [SerializeField]
    bool hasSpawnCell = false;

    [SerializeField, Header("Debug")]
    bool useDebug = true;

    public Cell this[int _index] => _index < 0 || _index >= cells.Length ? cells[0] : cells[_index];

    Vector3 SpawnOrigin => transform.position + new Vector3(-(xSize / 2f), 0f, zSize/2f);

    #endregion

    #region UnityEvents
    private void Start() => BindCellsToBoard();

    private void OnDrawGizmos()
    {
        if (!useDebug)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(xSize, 0.01f, zSize));

        for (int i = 0; i < cells.Length; ++i)
        {
            if (cells[i] == null)
                continue;

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(cells[i].transform.position, 0.01f);
        }
    }
    #endregion

    #region CustomMethods
    void BindCellsToBoard()
    {
        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            Cell _cell = cells[i];

            if(_cell)
                _cell.OnCellActionEnd += () => OnCellTriggered?.Invoke(_cell);
        }
    }

    public void SpawnCells()
    {
        if (hasSpawnCell)
            return;

        for(int i = 0; i < BOARD_SIZE; ++i)
            cells[i] = CreateCell(i);

        hasSpawnCell = true;
    }

    /// <summary>
    /// Create a new cell with a specific position, rotation and name depending on index.
    /// Create by default a TaxCell
    /// </summary>
    /// <param name="_index">Index of the cell</param>
    /// <returns></returns>
    Cell CreateCell(int _index)
    {
        Cell _cell = new GameObject().AddComponent<TaxCell>();
        _cell.transform.position = GetCellPosition(_index);
        _cell.transform.eulerAngles = GetCellEulerAngles(_index);
        _cell.transform.SetParent(transform);
        _cell.name = $"Cell_{_index}";

        return _cell;
    }

    /// <summary>
    /// Returns cell positiuon on the board based on index and size of the board.
    /// Compute a percentage of the X and Z axis on the board based on index
    /// Then multiply this percentage by size in order to get position on X and Z axis
    /// </summary>
    /// <param name="_index">Index on the board</param>
    /// <returns>Position of the cell on the board</returns>
    Vector3 GetCellPosition(int _index)
    {
        Vector3 _cellOffset = Vector3.zero;

        float _percentX = 0f,
              _percentZ = 0f;

        if (_index > BOARD_HALFSIZE)
        {
            int _tempIndex = _index - BOARD_HALFSIZE;

            float _diffIndex = (BOARD_LINE - _tempIndex);

            _percentX = _diffIndex < 0 ? 0f : _diffIndex / BOARD_LINE;
            _percentZ = _tempIndex <= BOARD_LINE ? 1f : (float)(BOARD_LINE - (_tempIndex % BOARD_LINE)) / BOARD_LINE;
        }
        else
        {
            float _diffIndex = (_index - BOARD_LINE);

            _percentX = _index > BOARD_LINE ? 1f : (float)_index / BOARD_LINE;
            _percentZ = _diffIndex < 0 ? 0f : _diffIndex / BOARD_LINE;
        }

        _cellOffset.x = xSize * _percentX;
        _cellOffset.z = zSize * -(_percentZ);

        return SpawnOrigin + _cellOffset;
    }

    /// <summary>
    /// Based on index, returns a value in <see cref="YAW_ROTATIONS_CELL"/>
    /// </summary>
    /// <param name="_index">Index on the board</param>
    /// <returns>Rotation of the cell on the board</returns>
    Vector3 GetCellEulerAngles(int _index)
    {
        int _currentLine = _index / BOARD_LINE;
        _currentLine = _currentLine > 3 ? 3 : _currentLine;

        return new Vector3(0f, YAW_ROTATIONS_CELL[_currentLine], 0f);
    }

    public void TriggerCellEvent(MonopolyCharacter _instigator, int _index)
    {
        cells[_index].PlayCellEffect(_instigator);
    }
    #endregion
}
