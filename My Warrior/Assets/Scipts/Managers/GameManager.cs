using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                {
                    checkpoint.ActivateCheckpoint();
                }
            }
        }

        closestCheckpointId = _data.closestCheckpointId;
        Invoke("PlacePlayerAtClosestCheckpoint", .1f);
    }

    private void PlacePlayerAtClosestCheckpoint()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
                PlayerManager.instance.player.transform.position = checkpoint.transform.position;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (FindClosestCheckpoint() == null)
        {
            _data.closestCheckpointId = "";
        }
        else
        {
            _data.closestCheckpointId = FindClosestCheckpoint().id;

        }

        _data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)
        {
            float distanceFromPlayerToCheckpoint = Vector2.
                Distance(PlayerManager.instance.player.transform.position, checkpoint.transform.position);

            if (distanceFromPlayerToCheckpoint < closestDistance && checkpoint.activationStatus == true)
            {
                closestDistance = distanceFromPlayerToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
