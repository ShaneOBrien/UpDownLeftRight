using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UpDownLeftRight.Editor
{

	[CustomEditor(typeof(CheatCodeSettings))]
	public class CheatCodeSettingsEditor : UnityEditor.Editor
	{
		private List<CheatCode> cheatCodes = new List<CheatCode>();
		GUIContent cheatLenghtLabel = new GUIContent("Number of steps [?]:", "The number of buttons you have to press before your cheat codes activate");
		GUIContent userFriendlyLabel = new GUIContent("Name [?]:", "The name of the cheat in a human readable format");
		GUIContent shortCodeLabel = new GUIContent("ShortCode [?]:", "Computer friendly name, makes it easy to compare in if statements");

		const string UnityAssetFolder = "Assets";

		//Creates the asset file
		public static CheatCodeSettings GetOrCreateSettingsAsset()
		{
			string fullPath = Path.Combine(Path.Combine(UnityAssetFolder, CheatCodeSettings.cheatCodeSettingsPath),
										   CheatCodeSettings.cheatCodeSettingsAssetName + CheatCodeSettings.cheatCodeSettingsAssetExtension
										   );

			CheatCodeSettings instance = AssetDatabase.LoadAssetAtPath(fullPath, typeof(CheatCodeSettings)) as CheatCodeSettings;

			if (instance == null)
			{
				// no asset found, we need to create it. 

				if (!Directory.Exists(Path.Combine(UnityAssetFolder, CheatCodeSettings.cheatCodeSettingsPath)))
				{
					AssetDatabase.CreateFolder(Path.Combine(UnityAssetFolder, "UpDownLeftRight"), "Resources");
				}

				instance = CreateInstance<CheatCodeSettings>();
				AssetDatabase.CreateAsset(instance, fullPath);
				AssetDatabase.SaveAssets();
			}
			return instance;
		}

		[MenuItem("Cheat Codes/Edit Settings")]
		public static void Edit()
		{
			Selection.activeObject = GetOrCreateSettingsAsset();
		}

		void OnDisable()
		{
			// make sure the runtime code will load the Asset from Resources when it next tries to access this. 
			CheatCodeSettings.SetInstance(null);
		}
		public override void OnInspectorGUI()
		{

			CheatCodeSettings settings = (CheatCodeSettings)target;
			CheatCodeSettings.SetInstance(settings);
			cheatCodes = CheatCodeSettings.CheatCodes;

			EditorGUILayout.LabelField("Cheat Codes");

			EditorGUILayout.HelpBox("A list of all cheat codes", MessageType.None);

			CheatCodeSettings.CheatCodeLength = EditorGUILayout.IntField(cheatLenghtLabel, CheatCodeSettings.CheatCodeLength);

			EditorGUILayout.Space();

			for (int i = 0; i < cheatCodes.Count; i++)
			{
				EditorGUILayout.LabelField("Cheat Code " + (i+1) + " - " + cheatCodes[i].name);
				cheatCodes[i].name = EditorGUILayout.TextField(userFriendlyLabel, cheatCodes[i].name);
				cheatCodes[i].shortCode = EditorGUILayout.TextField(shortCodeLabel, cheatCodes[i].shortCode);

				EditorGUILayout.LabelField("Cheat Code Sequence");

				for (int o = 0; o < CheatCodeSettings.CheatCodeLength; o++)
				{
					GUIContent step = new GUIContent("Step " + (o + 1));
					cheatCodes[i].cheatCodeSequence[o] = (KeyCode)EditorGUILayout.EnumPopup(step, cheatCodes[i].cheatCodeSequence[o]);
				}

				if (GUILayout.Button("Delete this cheat code!"))
				{
					cheatCodes.Remove(cheatCodes[i]);
					SaveAsset(settings);
				}

				EditorGUILayout.Space();
			}

			if (GUILayout.Button("Add New Cheat Code"))
			{
				CheatCode newCheatCode = new CheatCode();
				newCheatCode.name = "New Cheat Code";
				newCheatCode.shortCode = "newCheatCode";
				newCheatCode.cheatCodeSequence = new List<KeyCode>(CheatCodeSettings.CheatCodeLength);

				//prepopulate with empty strings, otherwise gives error as setting the capacity above does nothing
				for (int i = 0; i < CheatCodeSettings.CheatCodeLength; i++)
				{
					newCheatCode.cheatCodeSequence.Add(KeyCode.A);
				}
                cheatCodes.Add(newCheatCode);

				SaveAsset(settings);
			}

			if (GUI.changed)
			{
				Debug.Log("gui changed");
				SaveAsset(settings);
			}

		}
		public void SaveAsset(CheatCodeSettings settings)
		{
			CheatCodeSettings.CheatCodes = cheatCodes;
			EditorUtility.SetDirty(settings);
			AssetDatabase.SaveAssets();
		}
	}
}