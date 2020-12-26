using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadProjectMenu : MonoBehaviour {
	public Button projectButtonPrefab;
	public Transform scrollHolder;
	[SerializeField, HideInInspector]
	List<Button> loadButtons, deleteButtons;

	void OnEnable () {
		loadButtons.Clear();
		deleteButtons.Clear();
		foreach (Transform child in scrollHolder)
		{
			Destroy(child.gameObject);
		}
		string[] projectNames = SaveSystem.GetSaveNames ();

		for (int i = 0; i < projectNames.Length; i++) {
			string projectName = projectNames[i];
			if (i >= loadButtons.Count) {
				loadButtons.Add (Instantiate (projectButtonPrefab, parent : scrollHolder));
			}
			Button loadButton = loadButtons[i];
			loadButton.GetComponentInChildren<TMPro.TMP_Text> ().text = projectName.Trim ();
			loadButton.onClick.RemoveAllListeners();
			loadButton.onClick.AddListener (() => LoadProject (projectName));

			var deleteButton = loadButton.transform.Find("Delete Button").GetComponentInChildren<Button>();
			deleteButton.onClick.RemoveAllListeners();
			deleteButton.onClick.AddListener(() => DeleteProject(projectName));
			deleteButtons.Add(deleteButton);
		}
	}
	public void LoadProject (string projectName) {
		SaveSystem.SetActiveProject (projectName);
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
	public void DeleteProject(string projectName)
	{
		SaveSystem.DeleteProject(projectName);
		OnEnable();
	}
}