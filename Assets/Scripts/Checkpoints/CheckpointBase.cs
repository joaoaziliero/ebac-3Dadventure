using System.IO;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public int checkpointNumber;
    public string saveFile = "PlayerData.json";

    private void Start()
    {
        //var lastSaveComparision = CompareLatestSave(Path.Combine(Application.persistentDataPath, saveFile));
        //GetComponent<Collider>().enabled = !lastSaveComparision;
        //if (lastSaveComparision == true) ConfirmCheckpointUse();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveGame(other.gameObject.GetComponent<SaveManager>());
            ConfirmCheckpointUse();
        }
    }

    private bool CompareLatestSave(string path)
    {
        if (File.Exists(path) == false)
        {
            return false;
        }
        else
        {
            var latestCheckpoint = JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(path)).latestCheckpoint;
            return checkpointNumber <= latestCheckpoint;
        }
    }

    private void SaveGame(SaveManager saveManager)
    {
        //saveManager.Save(checkpointNumber);
    }

    protected virtual void ConfirmCheckpointUse() { }
}
