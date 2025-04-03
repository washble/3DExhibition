using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool useVsync = true;
    
    protected override void Awake()
    {
        base.Awake();

        // Temp vsync and targetFrameRate settings;
        if (useVsync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }
    }

    [SerializeField] private GameObject player;
    public GameObject Player => player;

    [SerializeField] private PlayerWeaponController playerWeaponController;
    public PlayerWeaponController PlayerWeaponController => playerWeaponController;
}

public enum GameObjectLayer
{
    Default = 0,
    Player = 3,
    Map = 6,
    Enemy = 7,
    Weapon = 8
}

public enum GameObjectTag
{
    Player,
    Gunslinger,
    Shielder
}

public enum WeaponType
{
    PlayerSword,
    PlayerRifleBullet,
    GunslingerBullet,
    CompanionShield,
    CompanionSword,
}