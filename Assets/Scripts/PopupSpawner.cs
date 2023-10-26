using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeauRoutine;

public class PopupSpawner : Singleton<PopupSpawner>
{
    [SerializeField] private Transform popupParent;
    [SerializeField] private GameObject[] popupPrefabs;
    [SerializeField] private float minRandSpawnTime;
    [SerializeField] private float maxRandSpawnTime;

    Routine currRoutine = Routine.Null;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private List<Routine> speedupRoutines = new List<Routine>();
    private int numSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        currRoutine = Routine.Start(this, SpawnPopupsRoutine());
    }

    public void RestartRoutine()
    {
        currRoutine.Stop();

        foreach (GameObject obj in spawnedObjects)
            Destroy(obj);

        numSpawned = 0;

        currRoutine = Routine.Start(this, SpawnPopupsRoutine());
    }

    public void RemoveFromList(GameObject obj)
    {
        spawnedObjects.Remove(obj);
    }

    private IEnumerator SpawnPopupsRoutine()
    {
        while(true)
        {
            float timeToWait = Random.Range(minRandSpawnTime, maxRandSpawnTime);
            yield return timeToWait;

            int randIndex = Random.Range(0, popupPrefabs.Length - 1);

            GameObject newPopup = Instantiate(popupPrefabs[randIndex], popupParent);
            newPopup.transform.localPosition = new Vector3(0, 0, -numSpawned/100f);

            newPopup.transform.GetChild(0).GetComponent<PopupX>().AfterSpawned();

            numSpawned++;
            spawnedObjects.Add(newPopup);
        }
    }

    public void RemoveAllPopups()
    {
        foreach (Routine routine in speedupRoutines)
            routine.Stop();


        speedupRoutines = new List<Routine>();
        RestartRoutine();

        Time.timeScale = 1;
    }

    public void SpeedUp()
    {
        speedupRoutines.Add(Routine.Start(this, SpeedRoutine()));
    }

    private IEnumerator SpeedRoutine()
    {
        Time.timeScale += 0.5f;

        yield return 5;
        Time.timeScale -= 0.5f;
    }

    private void OnDestroy()
    {
        RemoveAllPopups();
        Time.timeScale = 1;
    }
}
