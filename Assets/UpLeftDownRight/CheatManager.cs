using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CheatManager : MonoBehaviour {

	public List<string> lastInputs = new List<string>();

	public List<List<string>> cheatCodes = new List<List<string>>();

	public int cheatCodeLength;

	// Use this for initialization
	void Start () {

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
	
	// Update is called once per frame
	void Update () {
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(key))
			{
				lastInputs.Add(key.ToString());
				foreach (List<string> cheatCode in cheatCodes)
				{
					if (Enumerable.SequenceEqual(lastInputs.OrderBy(t => t), cheatCode.OrderBy(t => t)))
					{
						Debug.Log("We Got a match!");
						lastInputs.Clear();
					}
				}
				
			}
		}
	}
}
