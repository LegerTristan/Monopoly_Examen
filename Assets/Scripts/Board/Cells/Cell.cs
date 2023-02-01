using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityToolbox.GizmosTools;

/// <summary>
/// Base class for all cells.
/// Contains a name and play a cell effect, can also print cell effect.
/// </summary>
public abstract class Cell : MonoBehaviour
{
    public event Action OnCellActionEnd = null;
    //const float DEFAULT_FORWARD_CELL_OFFSET = -0.015f;

    //[SerializeField, Header("Cell")]
    //Offset cellOffset = new Offset(new Vector3(0f, 0f, DEFAULT_FORWARD_CELL_OFFSET), true);

    //[SerializeField, Header("Debug")]
    //Color cellPosDebugColor = Color.cyan;

    //[SerializeField, Range(0.01f, 1f)]
    //float cellPosDebugSize = 0.01f;

    //[SerializeField]
    //bool useDebug = true;

    //protected Vector3 CellPosition => transform.position + cellOffset.GetOffset(transform);

    protected abstract string CellName { get; }

    public abstract void PlayCellEffect(MonopolyCharacter _instigator);

    protected void PrintCellEffect(string _text, Color _color) => 
        MonopolyUIManager.Instance?.TurnInfoPanel?.SetTxtTurn(_text, _color);

    /// <summary>
    /// Used in order to invoke OncellActionEnd for child classes
    /// </summary>
    protected virtual void EndCellAction()
    {
        OnCellActionEnd?.Invoke();
    }

    //protected virtual void OnDrawGizmos()
    //{
    //    if (!useDebug)
    //        return;

    //    GizmosUtils.DrawWireSphere(CellPosition, cellPosDebugSize, cellPosDebugColor);
    //}
}
