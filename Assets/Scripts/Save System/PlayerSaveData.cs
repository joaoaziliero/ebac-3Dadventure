using System;

[Serializable]
public class PlayerSaveData
{
    public string coins;
    public int health;
    public int skinIndex;
    public int latestCheckpoint;
    public float X_Position;
    public float Y_Position;
    public float Z_Position;

    public int loadedAlready = 0; // false by default
}
