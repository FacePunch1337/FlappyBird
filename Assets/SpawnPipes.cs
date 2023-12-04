using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPipes : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject[] pipes;
    [SerializeField] private GameObject[] bugs;
    private float up = 10.5f;
    private float down = 7f;
    private float spawnInterval = 2.5f;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        StartCoroutine(SpawnPipeRoutine());
        StartCoroutine(SpawnBugRoutine());


    }

  

    private IEnumerator SpawnPipeRoutine()
    {
        while (true)
        {
             if (gameManager.StartGame)
             {
                 SpawnPipe();
             }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    private IEnumerator SpawnBugRoutine()
    {
        while (true)
        {
            if (gameManager.StartGame)
            {
                SpawnBug();
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    private void SpawnPipe()
    {
        int randomIndex = UnityEngine.Random.Range(0, pipes.Length);
        float randomHeight = UnityEngine.Random.Range(down, up);

        Vector3 spawnPosition = new Vector3(10f, randomHeight, 0); 

        GameObject pipe = Instantiate(pipes[randomIndex], spawnPosition, Quaternion.identity);
        spawnedObjects.Add(pipe);
    }

    private void SpawnBug()
    {
        
        int randomIndex = UnityEngine.Random.Range(0, bugs.Length);
        float randomHeight = UnityEngine.Random.Range(-4f, 4f);

        Vector3 spawnPosition = new Vector3(17f, randomHeight, 0);

        GameObject bug = Instantiate(bugs[randomIndex], spawnPosition, Quaternion.identity);
        spawnedObjects.Add(bug);
    }

    private void Update()
    {
        
        MoveObjects();
    }

    private void MoveObjects()
    {
        float leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 5f;


       
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnedObjects[i];

            obj.transform.Translate(Vector3.left * gameManager.Speed * Time.deltaTime);


            if (obj.transform.position.x < leftEdge)
            {
                spawnedObjects.RemoveAt(i);
                Destroy(obj);
       
            }
           
        }
    }

   


}

