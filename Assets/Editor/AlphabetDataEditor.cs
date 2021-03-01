using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

[CustomEditor(typeof(AlphabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlphabetDataEditor : Editor
{
	private AlphabetData DataInstance
	{
		get => target as AlphabetData;
	}


	private ReorderableList _alphabetPlainList;
	private ReorderableList _alphabetNormalList;
	private ReorderableList _alphabetHighlightedList;
	private ReorderableList _alphabetWrongList;

	private int toolbarSelected = 0;

	private void OnEnable()
	{
		InitializeReorderableList(ref _alphabetPlainList, "_alphabetPlain", "Plain");
		InitializeReorderableList(ref _alphabetNormalList, "_alphabetNormal", "Normal");
		InitializeReorderableList(ref _alphabetHighlightedList, "_alphabetHighlighted", "Highlighted");
		InitializeReorderableList(ref _alphabetWrongList, "_alphabetWrong", "Wrong");
	}





	public override void OnInspectorGUI()
	{
		serializedObject.Update();


		#region Отрисовка контролов
		// --- [     Тулбар     ] --- //
		toolbarSelected = GUILayout.Toolbar(toolbarSelected, new[] {"Plain", "Normal", "Highlighted", "Wrong"});
		switch (toolbarSelected)
		{
			case 0: // --- [    Вкладка "Plain"    ] --- //
				_alphabetPlainList.DoLayoutList();
				break;
			case 1: // --- [   Вкладка "Normal"    ] --- //
				_alphabetNormalList.DoLayoutList();
				break;
			case 2: // --- [ Вкладка "Highlighted" ] --- //
				_alphabetHighlightedList.DoLayoutList();
				break;
			case 3: // --- [    Вкладка "Wrong"    ] --- //
				_alphabetWrongList.DoLayoutList();
				break;
		}
		#endregion



		serializedObject.ApplyModifiedProperties();
		if (GUI.changed)
		{
			EditorUtility.SetDirty(DataInstance);
			//EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}

	private void InitializeReorderableList(ref ReorderableList list, string propertyName, string listLabel)
	{
		list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName));
		list.elementHeight = EditorGUIUtility.singleLineHeight * 4f; 



		list.drawHeaderCallback = rect =>
		{
			EditorGUI.LabelField(rect, listLabel);
		};

		var t_list = list;

		list.drawElementCallback = (rect, index, active, focused) =>
		{
			var element = t_list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 4;

			EditorGUILayout.BeginVertical();
			EditorGUI.LabelField(
					new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), 
				$"Буква {index + 1}");


			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 5f, 60, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("_letter"), GUIContent.none);
			EditorGUILayout.EndVertical();


			EditorGUI.ObjectField(
				new Rect(rect.width - 32, rect.y, 64, 64),
				element.FindPropertyRelative("_image"), typeof(Sprite), GUIContent.none);
		};
	}
}
