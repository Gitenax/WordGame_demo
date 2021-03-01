using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;


public class TempDataEditorWindow : EditorWindow
{
	public static void Open(TempData dataObject)
	{
		var window = GetWindow<TempDataEditorWindow>($"Редактор задниц | {dataObject.name}");
	}

}
