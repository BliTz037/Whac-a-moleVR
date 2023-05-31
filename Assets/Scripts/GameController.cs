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

    int score;

    public Transform spawnPoints;
    public GameObject molePrefab;

    public List<Vector3> SpawnPointList = new List<Vector3>();

    public List<GameObject> MolesInScene = new List<GameObject>();

    public Transform playerVR;

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
            currentMole.SetActive(true);
            MolesInScene.Add(currentMole);
        } else {
            Debug.Log("Max moles in scene");
        }
    }

    void KillMole(GameObject _mole) {
        SpawnPointList.Add(_mole.transform.localPosition);
        MolesInScene.Remove(_mole);
        Destroy(_mole);
        score++; 
    }
}
