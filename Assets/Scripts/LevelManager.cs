using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;
    public int piecesNumberStart = 3;
    public int piecesNumber = 5;
    public int piecesNumberEnd = 1;

    [SerializeField] private int _index;

    private GameObject _currentLevel; 
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();


    private void Awake() {
        //SpawnNextLevel();
        CreateLevelPieces();
    }
    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();

        for(int i = 0; i < piecesNumberStart; i++)
        {
            CreateLevelPiece(levelPiecesStart);
        }
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
        }
        for(int i = 0; i < piecesNumberEnd; i++)
        {
            CreateLevelPiece(levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(.artType);
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }
    #endregion

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}