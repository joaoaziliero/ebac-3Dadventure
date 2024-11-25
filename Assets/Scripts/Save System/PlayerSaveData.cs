using System;

[Serializable]
public class PlayerSaveData
{
    public string coins;
    public string berries;
    public int health;
    public int skinIndex;
    public float X_Position;
    public float Y_Position;
    public float Z_Position;

    public int loadedAlready = 0; // false by default
}
