using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler : MonoBehaviour
{
	[OnOpenAsset()]
	public static bool OpenEditor(int instance, int line)
	{
		var selected = EditorUtility.InstanceIDToObject(instance);


		if (selected as BoardData != null)
		{
			BoardDataEditorWindow.Open((BoardData)selected);
			return true;
		}
		else if (selected as TempData != null)
		{
			TempDataEditorWindow.Open((TempData)selected);
			return true;
		}

		return false;
	}
}
