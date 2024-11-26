using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public string saveFile = "PlayerData.json";
    private SaveManager _saveManager = null;

    private void Awake()
    {
        saveFile = Path.Combine(Application.persistentDataPath, saveFile);
    }

    private void Update()
    {
        if (_saveManager == null && SceneManager.GetActiveScene().buildIndex > 0)
        {
            _saveManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SaveManager>();
        }
    }

    public void SaveGame()
    {
        var latestCheckpoint = File.Exists(saveFile) ?
            JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(saveFile)).latestCheckpoint : 0;

        _saveManager.Save(latestCheckpoint);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
