using System;
using System.Collections.Generic;

/// <summary>
/// Class containing all the data the game needs to work, also some session specific data.
/// </summary>
[Serializable]
public class GameData
{
    public float ScoreTileInterval;
    /// <summary>
    /// Speed the enemy has when further than EnemyNearbyDistance away.
    /// </summary>
    public float EnemySpeedFar;
    /// <summary>
    /// Speed the enemy has when less than EnemyNearbyDistance away.
    /// </summary>
    public float EnemySpeedNearby;
    /// <summary>
    /// Distance the enemy has to be away for it to switch its speed.
    /// </summary>
    public float EnemyNearbyDistance;
    /// <summary>
    /// The degree of randomness the enemy uses for movement when in the Nearby state.
    /// </summary>
    public float EnemyRandomness;
    /// <summary>
    /// Factor of the camera speed the enemy uses.
    /// </summary>
    public float EnemyCameraSpeedFactor;
    /// <summary>
    /// Speed of the camera.
    /// </summary>
    public float CameraSpeed;
    /// <summary>
    /// Speed of the player.
    /// </summary>
    public float PlayerSpeed;
    /// <summary>
    /// Coins the player has in the current session.
    /// </summary>
    public int Coins;
    /// <summary>
    /// Close calls the player currently has.
    /// </summary>
    public int CloseCalls;
}

[Serializable]
public enum Controls
{
    DPads = 0,
    Joystick
}

/// <summary>
/// Class containing all data that needs to be saved and is player specific.
/// </summary>
[Serializable]
public class SaveData
{
    public bool HideTutorials;
    public bool JoystickVisible;
    public Controls SelectedControls;
    /// <summary>
    /// Name of the player for high score purposes.
    /// </summary>
    public string PlayerName;
    /// <summary>
    /// Total coins the player possesses.
    /// </summary>
    public int Coins;
    /// <summary>
    /// The player's personal highest score.
    /// </summary>
    public int PersonalBest;
}
