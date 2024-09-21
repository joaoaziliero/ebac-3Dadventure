using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    private bool _usedCheckpoint = false;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _usedCheckpoint == false)
        {
            SavePlayerPosition();
            ConfirmCheckpointUse();
        }
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("lastSavedPosition_X", transform.position.x);
        PlayerPrefs.SetFloat("lastSavedPosition_Y", transform.position.y);
        PlayerPrefs.SetFloat("lastSavedPosition_Z", transform.position.z);
    }

    protected virtual void ConfirmCheckpointUse()
    {
        PlayerPrefs.SetInt("isCheckpointAvailable", 1);
        _usedCheckpoint = true;
    }
}
