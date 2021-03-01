using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEditor;

public class BoardDataEditorWindow : EditorWindow
{
	public static void Open(BoardData dataObject)
	{
		var window = GetWindow<BoardDataEditorWindow>($"Редактор таблиц | {dataObject.name}");
	}

}
