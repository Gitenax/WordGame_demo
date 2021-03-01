using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Board Data", menuName = "Game Data/Board Data", order = 51)]
public class BoardData : ScriptableObject
{
	[SerializeField] private float timeInSeconds;
	[SerializeField] private int columns;
	[SerializeField] private int rows;
	[SerializeField] private BoardRow[] board;
	[SerializeField] private List<SearchingWord> searchWords = new List<SearchingWord>();

	#region Свойства
	public float TimeInSeconds
	{
		get => timeInSeconds;
		set => timeInSeconds = value;
	}
	public int Columns
	{
		get => columns;
		set => columns = value;
	}
	public int Rows
	{
		get => rows;
		set => rows = value;
	}
	public BoardRow[] Board
	{
		get => board;
		set => board = value;
	}
	public List<SearchingWord> SearchWords
	{
		get => searchWords;
		set => searchWords = value;
	}
	#endregion

	public void Clear()
	{
		for (int i = 0; i < columns; i++)
			board[i].ClearRow();
	}

	public void CreateNewBoard()
	{
		board = new BoardRow[columns];
		for (int i = 0; i < columns; i++)
			board[i] = new BoardRow(rows);
	}
}




[System.Serializable]
public class SearchingWord
{
	[SerializeField] private string _word;

	public string Word
	{
		get => _word;
		set => _word = value;
	}
}




[System.Serializable]
public class BoardRow
{
	public int Size;
	public string[] Row;

	public BoardRow() { }

	public BoardRow(int size)
	{
		Size = size;
		CreateRow(Size);
	}

	public void CreateRow(int size)
	{
		Row = new string[size];
		ClearRow();
	}

	public void ClearRow()
	{
		for (int i = 0; i < Row.Length; i++)
			Row[i] = string.Empty;
	}
}


