using System.Collections;
using System.Collections.Generic;
using UnityEditor;


// TODO => Remove Cell inheritance from MonoBehaviour and use Reflection and attribute to edit Cell parameters
//         without modifying position

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
    Board board = null;

    private void OnEnable()
    {
        board = (Board)target;

        if (board)
            board.SpawnCells();
    }
}
