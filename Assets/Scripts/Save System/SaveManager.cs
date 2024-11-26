using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string saveFile = "PlayerData.json";

    private void Awake()
    {
        saveFile = Path.Combine(Application.persistentDataPath, saveFile);
        OnAwakeLoad();
    }

    public void Save(int checkpoint)
    {
        PlayerSaveData playerSave = new()
        {
            coins = GetComponentsInChildren<TextMeshProUGUI>()[0].text,
            berries = GetComponentsInChildren<TextMeshProUGUI>()[1].text,
            health = GetComponentInChildren<PlayerHealth>().currentLifePoints.Value,
            skinIndex = GetComponentInChildren<ClothingManager>().SkinIndex,
            latestCheckpoint = checkpoint,
            X_Position = transform.position.x,
            Y_Position = transform.position.y,
            Z_Position = transform.position.z,
        };

        File.WriteAllText(saveFile, JsonUtility.ToJson(playerSave, true));
    }

    public void OnAwakeLoad()
    {
        var data = RetrieveData();

        if (data != null)
        {
            if (data.loadedAlready == 1) return;

            GetComponentsInChildren<TextMeshProUGUI>()[0].text = data.coins;
            GetComponentsInChildren<TextMeshProUGUI>()[1].text = data.berries;
            GetComponentInChildren<PlayerHealth>().currentLifePoints = new R3.ReactiveProperty<int>(data.health);
            GetComponentInChildren<ClothingManager>().SelectSkin(GetComponentInChildren<ClothingManager>().clothes[data.skinIndex]);
            transform.position = new Vector3(data.X_Position, data.Y_Position, data.Z_Position);

            data.loadedAlready = 1;
            File.WriteAllText(saveFile, JsonUtility.ToJson(data, true));
        }
    }

    private PlayerSaveData RetrieveData()
    {
        if (File.Exists(saveFile) == false) return null;

        return JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(saveFile));
    }

    private void OnApplicationQuit()
    {
        var data = RetrieveData();

        if (data != null)
        {
            data.loadedAlready = 0;
            File.WriteAllText(saveFile, JsonUtility.ToJson(data, true));
        }
    }
}
