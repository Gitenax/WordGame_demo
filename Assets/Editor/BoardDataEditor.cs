using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Random = UnityEngine.Random;

[CustomEditor(typeof(BoardData))]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataEditor : Editor
{
	private BoardData DataInstance => target as BoardData;
	private ReorderableList _dataOrder;

	private void OnEnable()
	{
		InitializeReorderableList(ref _dataOrder, "searchWords", "Some label");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update(); // Обновление иследуменого скрипта


		#region Отрисовка контролов
		// --- [ Кнопка "Открыть в редакторе" ] --- //
		if (GUILayout.Button("Открыть в редакторе"))
			BoardDataEditorWindow.Open((BoardData)target);
		EditorGUILayout.Space(10);


		// --- [   Поля "Столбца / строки"    ] --- //
		DrawInputTableFields();
		EditorGUILayout.Space();


		// --- [      Кнопка "To Upper"       ] --- //
		ToUpperButton();


		// --- [     Таблица с символами      ] --- //
		if (DataInstance.Board != null && DataInstance.Columns > 0 && DataInstance.Rows > 0)
			DrawTable();
		EditorGUILayout.Space(10);


		// --- [     Кнопка "Очистиить"      ] --- //
		EditorGUILayout.BeginHorizontal();
		ClearBoardButton();


		// --- [    "Заполнить случайными"   ] --- //
		FillUpWithRandomLetters();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space(10);


		// --- [         Список слов          ] --- //
		_dataOrder.DoLayoutList();
		#endregion


		// Применение изменений и отметка для сцены, что состояние изменено
		serializedObject.ApplyModifiedProperties();
		if(GUI.changed)
			EditorUtility.SetDirty(DataInstance);
	}

	private void DrawInputTableFields()
	{
		var columns = DataInstance.Columns;
		var rows = DataInstance.Rows;

		DataInstance.Columns = EditorGUILayout.IntField("Столбцы", DataInstance.Columns);
		DataInstance.Rows = EditorGUILayout.IntField("Строки", DataInstance.Rows);

		if ((DataInstance.Columns != columns || DataInstance.Rows != rows)
		    && DataInstance.Columns > 0 && DataInstance.Rows > 0)
		{
			DataInstance.CreateNewBoard();
		}
	}

	private void DrawTable()
	{
		var tableStyle = new GUIStyle("box");
		tableStyle.padding = new RectOffset(10, 0, 10, 0);
		//tableStyle.margin.left = 32;

		var headerColumnStyle = new GUIStyle();
		headerColumnStyle.fixedWidth = 35;

		var columnStyle = new GUIStyle();
		columnStyle.fixedWidth = 50;

		var rowStyle = new GUIStyle();
		rowStyle.fixedHeight = 25;
		rowStyle.fixedWidth = 40;
		rowStyle.alignment = TextAnchor.MiddleCenter;

		var textFieldStyle = new GUIStyle();
		textFieldStyle.normal.background = Texture2D.grayTexture;
		textFieldStyle.normal.textColor = Color.white;
		textFieldStyle.fontStyle = FontStyle.Bold;
		textFieldStyle.alignment = TextAnchor.MiddleCenter;

		EditorGUILayout.BeginHorizontal(tableStyle);
		for (int x = 0; x < DataInstance.Columns; x++)
		{
			EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
			for (int y = 0; y < DataInstance.Rows; y++)
			{
				if (x >= 0 && y >= 0)
				{
					EditorGUILayout.BeginHorizontal(rowStyle);
					var character = (string) EditorGUILayout.TextArea(DataInstance.Board[x].Row[y], textFieldStyle);
					if (DataInstance.Board[x].Row[y].Length > 1)
					{
						character = DataInstance.Board[x].Row[y].Substring(0, 1);
					}
					DataInstance.Board[x].Row[y] = character;
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();
	}

	private void InitializeReorderableList(ref ReorderableList list, string propertyName, string listLabel)
	{
		list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName));

		list.drawHeaderCallback = rect =>
		{
			EditorGUI.LabelField(rect, listLabel);
		};

		var t_list = list;

		list.drawElementCallback = (rect, index, active, focused) =>
		{
			var element = t_list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;

			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("_word"), GUIContent.none);
		};
	}

	private void ToUpperButton()
	{
		if (GUILayout.Button("To Upper"))
		{
			for (int i = 0; i < DataInstance.Columns; i++)
			{
				for (int j = 0; j < DataInstance.Rows; j++)
				{
					if (Regex.Matches(DataInstance.Board[i].Row[j], @"[а-я]").Count > 0)
						DataInstance.Board[i].Row[j] = DataInstance.Board[i].Row[j].ToUpper();
				}
			}

			foreach (var searchWord in DataInstance.SearchWords)
			{
				if (Regex.Matches(searchWord.Word, @"[а-я]").Count > 0)
					searchWord.Word = searchWord.Word.ToUpper(); 
			}
		}
	}

	private void ClearBoardButton()
	{
		if (GUILayout.Button("Clear Board"))
		{
			for (int i = 0; i < DataInstance.Columns; i++)
			{
				for (int j = 0; j < DataInstance.Rows; j++)
				{
					DataInstance.Board[i].Row[j] = String.Empty;
				}
			}
		}
	}

	private void FillUpWithRandomLetters()
	{
		if (GUILayout.Button("Fill random"))
		{
			string letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

			for (int i = 0; i < DataInstance.Columns; i++)
			{
				for (int j = 0; j < DataInstance.Rows; j++)
				{
					if (Regex.Matches(DataInstance.Board[i].Row[j], @"[а-яА-Я]").Count == 0)
						DataInstance.Board[i].Row[j] = letters[UnityEngine.Random.Range(0, letters.Length)].ToString();
				}
			}
		}
	}
}
	