#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OtyColider2D))]
public class OtyColider2DEditor : Editor
{
    private void OnSceneGUI()
    {
        OtyColider2D col = (OtyColider2D)target;
        Vector3 pos = col.transform.position;

        Vector3 top = new(pos.x, pos.y + col.coliderBounder.upBoard);
        Vector3 bottom = new(pos.x, pos.y + col.coliderBounder.downBoard);
        Vector3 left = new(pos.x + col.coliderBounder.leftBoard, pos.y);
        Vector3 right = new(pos.x + col.coliderBounder.rightBoard, pos.y);

        EditorGUI.BeginChangeCheck();

        top = Handles.PositionHandle(top, Quaternion.identity);
        bottom = Handles.PositionHandle(bottom, Quaternion.identity);
        left = Handles.PositionHandle(left, Quaternion.identity);
        right = Handles.PositionHandle(right, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(col, "Adjust Collider Bounds");
            col.coliderBounder.upBoard = top.y - pos.y;
            col.coliderBounder.downBoard = bottom.y - pos.y;
            col.coliderBounder.leftBoard = left.x - pos.x;
            col.coliderBounder.rightBoard = right.x - pos.x;
            EditorUtility.SetDirty(col);
        }

        Handles.color = Color.yellow;
        Vector3 topLeft = new(pos.x + col.coliderBounder.leftBoard, pos.y + col.coliderBounder.upBoard);
        Vector3 topRight = new(pos.x + col.coliderBounder.rightBoard, pos.y + col.coliderBounder.upBoard);
        Vector3 bottomLeft = new(pos.x + col.coliderBounder.leftBoard, pos.y + col.coliderBounder.downBoard);
        Vector3 bottomRight = new(pos.x + col.coliderBounder.rightBoard, pos.y + col.coliderBounder.downBoard);

        Handles.DrawLine(topLeft, topRight);
        Handles.DrawLine(topRight, bottomRight);
        Handles.DrawLine(bottomRight, bottomLeft);
        Handles.DrawLine(bottomLeft, topLeft);
    }
}
#endif
