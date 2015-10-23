using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class CheatManager : MonoBehaviour {

	public List<string> lastInputs = new List<string>();

	public List<List<string>> cheatCodes = new List<List<string>>();

	public int cheatCodeLength;

	private bool canEnterCheats;

	public void Awake()
	{
		//Load cheat codes from resources file
	}

	public void Start()
	{
		List<string> code1 = new List<string>();
		code1.Add("A");
		code1.Add("B");
		code1.Add("C");
		code1.Add("D");
		code1.Add("E");
		code1.Add("F");
		code1.Add("G");
		code1.Add("H");
		code1.Add("I");
		code1.Add("J");

		cheatCodes.Add(code1);
	}

	public void Update()
	{
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(key))
			{
				lastInputs.Add(key.ToString());

				if (lastInputs.Count >= cheatCodeLength)
				{
					foreach (List<string> cheatCode in cheatCodes)
					{
						if (Enumerable.SequenceEqual(lastInputs, cheatCode))
						{
							Debug.Log("We Got a match!");
						}
						else
						{
							Debug.Log("No Match");
						}
					}
					lastInputs.Clear();
				}
			}
		}
	}

	public void EnableCheatEntering()
	{
		canEnterCheats = true;
		lastInputs.Clear();
	}

	public void DisableCheatEntering()
	{
		canEnterCheats = false;
		lastInputs.Clear();
	}
}
