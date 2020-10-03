using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ActionTableSpace;
using System;

[CustomEditor(typeof(TableData))]
public class InspectorTestManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _manager = (TableData)target;
        ShowData();
    }

    TableData _manager;
    int countColumn;
    int countRow;
    int countResult;
    Color[] colors = new Color[] { Color.white, Color.blue };

    CurrentSelect currentSelect;

    TypeTable currentTable = TypeTable.Table;


    public void ShowData() {
        countRow = Enum.GetNames(typeof(TypeObject)).Length;
        countColumn = Enum.GetNames(typeof(TypeAction)).Length;
        countResult = Enum.GetNames(typeof(TypeResult)).Length;
        _manager.Initialization();
        if (currentTable == TypeTable.Table)
        {
            ShowTable();
        }
        else
        {
            ShowSelect();
        }

    }

    private void ShowTable() {
        
        
        CreateFirstRow();
        CreateColumns();
    }

    private void ShowSelect() {
        GUIStyle style = GetStyle(TypeGUIStyle.Button);
        TypeResult _tempEnum;
        string nameButton;
        for (int i = 0; i < countResult; i++)
        {
            _tempEnum = (TypeResult)i;
            nameButton = _tempEnum.ToString();

            if (GUILayout.Button(nameButton, style))
            {
                _manager.SetData(currentSelect.typeObject, currentSelect.typeAction, _tempEnum);
                currentTable = TypeTable.Table;
            }
        }

        style = GetStyle(TypeGUIStyle.BackButton);
        
        
        GUILayout.Space(20);
        if (GUILayout.Button("Back", style))
        {
            currentTable = TypeTable.Table;
        }
    }



    private void CreateFirstRow() {
        GUIStyle style = GetStyle(TypeGUIStyle.EmptyBox);
        GUILayout.BeginHorizontal();
        GUILayout.Box("", style);
        style = GetStyle(TypeGUIStyle.Box);
        TypeAction _tempEnum;
        for (int i = 0; i < countColumn; i++)
        {
            _tempEnum = (TypeAction)i;
            GUILayout.Box(_tempEnum.ToString(), style);
        }
        
        GUILayout.EndHorizontal();
    }

    private void CreateColumns() {
        TypeObject _tempEnum;
        for (int i = 0; i < countRow; i++)
        {
            _tempEnum = (TypeObject)i;
            CreateRow(_tempEnum);
        }
    }

    private void CreateRow(TypeObject _typeObject)
    {
        GUIStyle style = GetStyle(TypeGUIStyle.Box);
        GUILayout.BeginHorizontal();
        GUILayout.Box(_typeObject.ToString(), style);
        style = GetStyle(TypeGUIStyle.Button);
        TypeResult _tempEnum;
        string nameButton;
        for (int i = 0; i < countColumn; i++)
        {
             _tempEnum = _manager.GetData(_typeObject, (TypeAction)i);
            nameButton = _tempEnum.ToString();
            
            if (GUILayout.Button(nameButton, style))
            {
                currentSelect = new CurrentSelect(_typeObject, (TypeAction)i);
                currentTable = TypeTable.Select;
            }
        }

        GUILayout.EndHorizontal();
    }

    private GUIStyle GetStyle(TypeGUIStyle typeGUI) {
        GUIStyle style;

        switch (typeGUI)
        {
            case TypeGUIStyle.Button:
                style = new GUIStyle(GUI.skin.button);
                GUI.backgroundColor = Color.yellow;
                style.richText = true;
                style.fixedHeight = _manager.height;
                style.fixedWidth = _manager.width;
                style.normal.textColor = Color.gray;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleCenter;
                break;
            case TypeGUIStyle.BackButton:
                style = new GUIStyle(GUI.skin.button);
                GUI.backgroundColor = Color.grey;
                style.richText = true;
                style.fixedHeight = _manager.height;
                style.fixedWidth = _manager.width;
                style.normal.textColor = Color.white;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleCenter;
                break;
            case TypeGUIStyle.Box:
                style = new GUIStyle(GUI.skin.box);
                GUI.backgroundColor = Color.white;
                style.richText = true;
                style.fixedHeight = _manager.height;
                style.fixedWidth = _manager.width;
                style.normal.textColor = Color.blue;
                style.fontStyle = FontStyle.BoldAndItalic;
                style.alignment = TextAnchor.MiddleCenter;
                break;
            case TypeGUIStyle.EmptyBox:
                style = new GUIStyle(GUI.skin.box);
                GUI.backgroundColor = new Color(1, 1, 1, 0);
                style.richText = true;
                style.fixedHeight = _manager.height;
                style.fixedWidth = _manager.width;
                style.normal.textColor = Color.blue;
                style.alignment = TextAnchor.MiddleCenter;
                break;
            default:
                style = new GUIStyle(GUI.skin.box);
                style.fixedHeight = _manager.height;
                style.fixedWidth = _manager.width;
                style.normal.textColor = Color.blue;
                break;
        }

        return style;
    }
}
