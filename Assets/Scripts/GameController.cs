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
    public bool isPlaying = false;

    public Transform spawnPoints;
    public GameObject molePrefab;

    public List<Vector3> SpawnPointList = new List<Vector3>();

    public List<GameObject> MolesInScene = new List<GameObject>();


    [SerializeField]
    private ScoreManager _scoreManager;

    private void Start() {
        StartFunction();
    }

    void StartFunction() {
        score = 0;
        foreach (Transform item in spawnPoints) {
            SpawnPointList.Add(item.localPosition);
        }

        InvokeRepeating("GetMole",1f,3f);
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
            //Debug.Log("Max moles in scene");
        }
    }

    public void KillMole(GameObject _mole) {
        SoundManager.Instance.PlaySound(SoundManager.Instance.hitSound);
        SpawnPointList.Add(_mole.transform.localPosition);
        MolesInScene.Remove(_mole);
        Destroy(_mole);
        score++;
        _scoreManager.UpdateScore(score);
    }
}
