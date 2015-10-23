using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class CheatManager : MonoBehaviour {

	public List<KeyCode> lastInputs = new List<KeyCode>();

	public List<CheatCode> cheatCodes = new List<CheatCode>();

	private int cheatCodeLength;

	private bool canEnterCheats;

	public void Awake()
	{
		//Load cheat codes from resources file
		cheatCodes = CheatCodeSettings.CheatCodes;
		cheatCodeLength = CheatCodeSettings.CheatCodeLength;
	}

	public void Start()
	{

	}

	public void Update()
	{
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(key))
			{
				lastInputs.Add(key);

				Debug.Log(key);

				if (lastInputs.Count >= cheatCodeLength)
				{

					foreach (CheatCode cheatCode in cheatCodes)
					{
						if (Enumerable.SequenceEqual(lastInputs, cheatCode.cheatCodeSequence))
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
