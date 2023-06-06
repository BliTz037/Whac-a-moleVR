using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int score = 0;
    public int life = 0;
    public bool isPlaying = false;
    public float speedSpawn = 3f;
    public float increaseSpeed = 0.2f;

    public Transform spawnPoints;
    public GameObject molePrefab;
    public GameObject endGameScreen;
    public GameObject currentGameScreen;

    public List<Vector3> SpawnPointList = new List<Vector3>();

    public List<GameObject> MolesInScene = new List<GameObject>();


    [SerializeField]
    private ScoreManager _scoreManager;

    public void Start() {
        _scoreManager.UpdateLife(life);
        life = 3;
        score = 0;
        speedSpawn = 3f;
    }

    public void resetGame() {
        _scoreManager.UpdateLife(life);
        life = 3;
        speedSpawn = 3f;
        score = 0;
        StartFunction();
    }

    public void StartFunction() {
        _scoreManager.UpdateLife(life);
        _scoreManager.UpdateScore(score);
        foreach (Transform item in spawnPoints) {
            SpawnPointList.Add(item.localPosition);
        }

        InvokeRepeating("GetMole",1f,speedSpawn);
    }

    public void EndFunction() {
        _scoreManager.UpdateLife(life);
        _scoreManager.UpdateScore(score);
        CancelInvoke();
        WipeClean();
    }

    void GetMole() {
        if (SpawnPointList.Count > 0) {
            Vector3 randomPosition = SpawnPointList[Random.Range(0, SpawnPointList.Count)];

            SpawnPointList.Remove(randomPosition);

            GameObject currentMole = Instantiate(molePrefab,randomPosition,Quaternion.identity,transform);
            Vector3 newRotate = currentMole.transform.eulerAngles;
            newRotate.y = 90;
            currentMole.transform.eulerAngles = newRotate;

            currentMole.SetActive(true);
            MolesInScene.Add(currentMole);
        } else {
            life -= 1;
            if (life <= 0) {
                EndFunction();
                endGameScreen.SetActive(true);
                currentGameScreen.SetActive(false);
                
            }
            EndFunction();
            StartFunction();
            //speedSpawn = speedSpawn * 3f;
            // SoundManager.Instance.PlaySound(SoundManager.Instance.damageSound);
        }
    }

    public void WipeClean() {
         _scoreManager.UpdateLife(life);
        foreach (GameObject mole in MolesInScene) {
            Destroy(mole);
            SpawnPointList.Add(mole.transform.localPosition);
            // MolesInScene.Remove(mole);
        }
        MolesInScene.Clear();
        SpawnPointList.Clear();
    }

    public void KillMole(GameObject _mole) {
        SpawnPointList.Add(_mole.transform.localPosition);
        MolesInScene.Remove(_mole);
        Destroy(_mole);
        score++;
        SoundManager.Instance.PlaySound(SoundManager.Instance.hitSound);
        if (speedSpawn >= 1f) speedSpawn += increaseSpeed;
        _scoreManager.UpdateScore(score);
    }
}
