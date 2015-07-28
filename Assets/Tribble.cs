using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Tribble : MonoBehaviour {

	public Text OutputText;

	private string[] lines;
	private List<string> Words = new List<string>();
	/*private string[] Words = {
							 "dog",
							 "tree",
							 "old"
							 };*/
	// TODO: see if there is a better container for this xD
	private List<KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>> WordsToClear = new List<KeyValuePair<KeyValuePair<int,int>,KeyValuePair<int,int>>>();

	// Use this for initialization
	void Start () {
		ReadOutputText();

		// Test
		Words.Add("dog");
		Words.Add("tree");
		Words.Add("old");

		SetLetter(3, 0, "t");
		//Solve(3, 0);
		//ClearWords();
		WriteOutputText();
	}

	void Solve(int changedX, int changedY) {
		// Horizontal
		int startLetter = 0;
		string line = lines[changedY];
		int endLetter = line.Length - 1;
		string word;

		while (startLetter <= endLetter) {
			int length = endLetter - startLetter + 1;
			if (length < 3 || endLetter < changedX) {
				startLetter++;
				endLetter = line.Length - 1;
				continue;
			}
			word = line.Substring(startLetter, length);
			if (Words.Contains(word)) {
				KeyValuePair<int, int> start = new KeyValuePair<int,int> (startLetter, changedY);
				KeyValuePair<int, int> end = new KeyValuePair<int,int> (endLetter, changedY);
				KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>> wordToClear = new KeyValuePair<KeyValuePair<int,int>, KeyValuePair<int,int>> (start, end);
				WordsToClear.Add(wordToClear);
			}
			endLetter--;
		}

		// Vertical
		startLetter = 0;
		endLetter = lines.Length - 1;
		word = "";

		while (startLetter <= endLetter) {
			int length = endLetter - startLetter + 1;
			if (length < 3 || endLetter < changedY) {
				startLetter++;
				endLetter = lines.Length - 1;
				continue;
			}
			// Assemble word
			for (int i = startLetter; i <= endLetter; i++) {
				if (changedX > lines[i].Length) break;
				word += lines[i][changedX];
			}
			if (Words.Contains(word)) {
				KeyValuePair<int, int> start = new KeyValuePair<int,int> (changedX, startLetter);
				KeyValuePair<int, int> end = new KeyValuePair<int,int> (changedX, endLetter);
				KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>> wordToClear = new KeyValuePair<KeyValuePair<int,int>, KeyValuePair<int,int>> (start, end);
				WordsToClear.Add(wordToClear);
			}
			endLetter--;
		}
	}

	void ClearWords() {
		foreach (var word in WordsToClear) {
			for (int y = word.Key.Value; y <= word.Value.Value; y++) {
				for (int x = word.Key.Key; x <= word.Value.Key; x++) {
					var arr = lines[y].ToCharArray();
					arr[x] = ' ';
					lines[y] = new string(arr);
				}
			}
		}
		WordsToClear = new List<KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>>();
	}

	void ReadOutputText() {
		// Test
		string text = OutputText.text;
		lines = text.Split(new string[] {"\n", "\r\n"}, StringSplitOptions.None);
	}

	void WriteOutputText() {
		string text = "";
		foreach (string line in lines) {
			text += line + "\n";
		}
		OutputText.text = text;
	}

	void SetLetter(int x, int y, string letter) {
		lines[y].Insert(x, letter);
		WriteOutputText();
	}
}
