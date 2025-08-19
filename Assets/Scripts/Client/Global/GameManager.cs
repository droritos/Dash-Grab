using UnityEngine;
using Game.Terrain;
using Game.Client.Player;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] WorldCreator worldCreator;
    [SerializeField] PlayerManager playerManager;

    public Vector3 PlayerSpawnPoint { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void OnDisable()
    {
        EndGame();
    }

    // Add any game management methods here
    private void StartGame()
    {
        worldCreator.CreateWorld(out Vector3 spawnPoint);
        PlayerSpawnPoint = spawnPoint;
    }
    private void EndGame()
    {
        worldCreator.DestoryWorld();
    }

    private void OnValidate()
    {
        if(!worldCreator)
        {
            worldCreator = FindAnyObjectByType<WorldCreator>();
        }

        if (!playerManager)
        {
            playerManager = FindAnyObjectByType<PlayerManager>();
        }
    }
}
