using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Alphabet Data", menuName = "Game Data/Alphabet Data", order = 50)]
public class AlphabetData : ScriptableObject
{
	[SerializeField] private List<LetterData> _alphabetPlain = new List<LetterData>();
	[SerializeField] private List<LetterData> _alphabetNormal = new List<LetterData>();
	[SerializeField] private List<LetterData> _alphabetHighlighted = new List<LetterData>();
	[SerializeField] private List<LetterData> _alphabetWrong = new List<LetterData>();

	#region Свойства
	public List<LetterData> AlphabetPlain
	{
		get => _alphabetPlain;
		set => _alphabetPlain = value;
	}
	public List<LetterData> AlphabetNormal
	{
		get => _alphabetNormal;
		set => _alphabetNormal = value;
	}
	public List<LetterData> AlphabetHighlighted
	{
		get => _alphabetHighlighted;
		set => _alphabetHighlighted = value;
	}
	public List<LetterData> AlphabetWrong
	{
		get => _alphabetWrong;
		set => _alphabetWrong = value;
	}
	#endregion
}


[System.Serializable]
public class LetterData
{
	[SerializeField] private string _letter;
	[SerializeField] private Sprite _image;

	#region Свойства
	public string Letter
	{
		get => _letter;
		set => _letter = value;
	}
	public Sprite Image
	{
		get => _image;
		set => _image = value;
	}
	#endregion
}

