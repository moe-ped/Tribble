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

	// Use this for initialization
	void Start () {
		// Just in case it causes issues
		//InitializeOutputLetters();
		ReadOutputText();
		// Test
		Words.Add("dog");
		Words.Add("tree");
		Words.Add("old");
		//SetLetter(3, 0, "t".ToCharArray()[0]);
		Solve(3, 0);
		WriteOutputText();
	}

	void Solve(int changedX, int changedY) {
		int startLetter = 0;
		string line = lines[changedY];
		int endLetter = line.Length;
		while (startLetter != endLetter + 1) {
			int length = endLetter - startLetter;
			if (length < 3 || endLetter == changedX) {
				startLetter++;
				endLetter = line.Length;
				continue;
			}
			string word = line.Substring(startLetter, length);
			if (Words.Contains(word)) {
				Debug.Log(word);
			}
			endLetter--;
		}
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

	void SetLetter(int x, int y, char letter) {
		lines[y].Insert(x, letter.ToString());
		WriteOutputText();
	}
}
