using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

/// <summary>
/// GameSparks settings which are used with <see cref="GameSparksUnity"/> to 
/// connect to the GameSparks backend. 
/// </summary>

[Serializable]
public class CheatCode
{
	public string name, shortCode;
	public List<string> cheatCode;

}

public class CheatCodeSettings : ScriptableObject
{

	public const string cheatCodeSettingsAssetName = "CheatCodesSettings";
	public const string cheatCodeSettingsPath = "UpDownLeftRight/Resources";
	public const string cheatCodeSettingsAssetExtension = ".asset";	

	private static CheatCodeSettings instance;

	public static void SetInstance(CheatCodeSettings settings)
	{
		instance = settings;
	}

	public static CheatCodeSettings Instance
	{
		get
		{
			if (ReferenceEquals(instance, null))
			{
				instance = Resources.Load(cheatCodeSettingsAssetName) as CheatCodeSettings;
				if (ReferenceEquals(instance, null))
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<CheatCodeSettings>();
				}
			}
			return instance;
		}
	}

	[SerializeField]
	public List<CheatCode> cheatCodes = new List<CheatCode>();
	[SerializeField]
	public int cheatCodeLength = 10;

	public static List<CheatCode> CheatCodes
	{
		get { return Instance.cheatCodes; }
		set { Instance.cheatCodes = value; }
	}

	public static int CheatCodeLength
	{
		get { return Instance.cheatCodeLength; }
		set { Instance.cheatCodeLength = value; }
	}

}
