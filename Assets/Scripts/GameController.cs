using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public enum GameResult
    {
        Win, 
        Lose
    }

    public static event Action OnLevelRestarted;

    [SerializeField] private GameObject playerTemplate;
    [SerializeField] private BaseObject[] interactableObjects;
    [SerializeField] private Vector2 spawnBounds;
    private List<ObjectType> objectsSequence = new List<ObjectType>();
    private List<BaseObject> spawnedObjects = new List<BaseObject>();
    private int activatedObjectsCount;

    private void Awake()
    {
        BaseObject.OnTriggered += BaseObject_OnActivated;
#if UNITY_EDITOR || UNITY_STANDALONE
        Cursor.visible = false;
#endif
    }
    private void OnDestroy()
    {
        BaseObject.OnTriggered -= BaseObject_OnActivated;
    }

    private void Start()
    {
        SpawnObjectsAtStart();
        playerTemplate.transform.position = GetRandonPoint();
        StartGame();
    }


    private void SpawnObjectsAtStart()
    {
        if(interactableObjects == null || interactableObjects.Length == 0)
        {
            return;
        }

        for(int i=0; i< interactableObjects.Length; i++)
        {
            var obj = Instantiate(interactableObjects[i], GetRandonPoint(), Quaternion.identity);
            spawnedObjects.Add(obj);
        }
    }

    private void  RandomizePositions()
    {
        playerTemplate.transform.position = GetRandonPoint();
        for(int i=0; i< spawnedObjects.Count; i++)
        {
            spawnedObjects[i].Deactivate();
            spawnedObjects[i].transform.position = GetRandonPoint();
        }
    }

    #region Randomize Methods

    private List<Vector3> generetadPoints = new List<Vector3>();
    private Vector3 GetRandonPoint()
    {
        Vector3? result =null;

        if (generetadPoints.Count == 0)
        {
            result = new Vector3(Random.Range(-spawnBounds.x, spawnBounds.x), 0f, Random.Range(-spawnBounds.y, spawnBounds.y));
            generetadPoints.Add(result.Value);
        }
        else
        {
            while(result == null)
            {
                var point = new Vector3(Random.Range(-spawnBounds.x, spawnBounds.x), 0f, Random.Range(-spawnBounds.y, spawnBounds.y));
                if (IsPointNearAnother(point))
                {
                    continue;
                }
                else
                {
                    result = point;
                }
                
            }
        }
        return result.Value;
    }

    private bool IsPointNearAnother(Vector3 point)
    {
        for (int i=0; i< generetadPoints.Count; i++)
        {
            if((point - generetadPoints[i]).sqrMagnitude <=1f)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
    

    private void BaseObject_OnActivated(BaseObject interactableObject)
    {
        var index = activatedObjectsCount;
        if(index>= objectsSequence.Count) { return; }

        if(objectsSequence[index] == interactableObject.Type)
        {
            interactableObject.ActivatedResult(true);
            activatedObjectsCount++;
        }
        else
        {
            interactableObject.ActivatedResult(false);
            GameOver(GameResult.Lose);
        }

        if(activatedObjectsCount == objectsSequence.Count)
        {
            GameOver(GameResult.Win);
        }
    }

    private void RestartLevel()
    {
        OnLevelRestarted?.Invoke();
        ClearLevel();
        RandomizePositions();
        GenerateSequence();
    }

    private void ClearLevel()
    {
        generetadPoints.Clear();
        activatedObjectsCount = 0;
        objectsSequence.Clear();
    }

    private void StartGame()
    {
        ClearLevel();
        GenerateSequence();
    }

    private void GameOver(GameResult gameResult)
    {
        if (gameResult == GameResult.Win)
        {
            RestartLevel();
        }
        else if (gameResult == GameResult.Lose)
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                spawnedObjects[i].Deactivate();
            }
            activatedObjectsCount = 0;
        }
    }

    private void GenerateSequence()
    {
        if(interactableObjects == null || interactableObjects.Length == 0)
        {
            Debug.LogError("List of interactable objects is empty");
            return;
        }

        if(objectsSequence == null) { objectsSequence = new List<ObjectType>(); }

        objectsSequence.Clear();
        while(objectsSequence.Count < interactableObjects.Length)
        {
            var randomType = interactableObjects[Random.Range(0, interactableObjects.Length)].Type;
            if (objectsSequence.Contains(randomType))
            {
                continue;
            }

            objectsSequence.Add(randomType);
        }


        UIManager.Instance.InitGameScreen(objectsSequence);
    }
}
