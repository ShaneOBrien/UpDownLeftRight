using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UpDownLeftRight.Editor
{
	/// <summary>
	/// Editor class for <see cref="CheatCodeSettings"/>
	/// </summary>
	[CustomEditor(typeof(CheatCodeSettings))]
	public class CheatCodeSettingsEditor : UnityEditor.Editor
	{

		private bool recordCheatCodeSequence = false;
		private List<CheatCode> cheatCodes = new List<CheatCode>();
		GUIContent inspectorTitle = new GUIContent("Cheat Codes");
		GUIContent cheatLenghtLabel = new GUIContent("Cheat Code Steps[?]:", "The number of buttons you have to press before your cheat codes activate");

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
				EditorGUILayout.LabelField("User Friendly Name");
				cheatCodes[i].name = EditorGUILayout.TextArea(cheatCodes[i].name);
				EditorGUILayout.LabelField("Computer Friendly Name");
				cheatCodes[i].shortCode = EditorGUILayout.TextArea(cheatCodes[i].shortCode);
				EditorGUILayout.LabelField("Cheat Code Sequence");

				string sequence = "No code created!";

				EditorGUILayout.LabelField(sequence);

				if (GUILayout.Button("Record New Sequence"))
				{
					recordCheatCodeSequence = true;
				}
				
			}

			if (GUILayout.Button("Add New Cheat Code"))
			{
				CheatCode newCheatCode = new CheatCode();
				newCheatCode.name = "New Cheat Code";
				newCheatCode.shortCode = "newCheatCode";
				newCheatCode.cheatCode = new List<string>();
				cheatCodes.Add(newCheatCode);

				CheatCodeSettings.CheatCodes = cheatCodes;

				EditorUtility.SetDirty(settings);
				AssetDatabase.Refresh();
			}

			if (GUI.changed)
			{
				Debug.Log("gui changed");
				CheatCodeSettings.CheatCodes = cheatCodes;
				EditorUtility.SetDirty(settings);
				AssetDatabase.SaveAssets();
			}

		}

		public void Update()
		{
			if (recordCheatCodeSequence)
			{
				foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
				{
					
				}
			}
		}
	}
}