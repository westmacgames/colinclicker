using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Singleton for spawning objects.
[RequireComponent(typeof(AudioSource))]
public class ColinSpawner : MonoBehaviour
{
    public int score = 0;

    //Colin prefab.
    [SerializeField]
    private GameObject m_colinPrefab;

    //Amount of seconds until next colin is spawned.
    private float m_spawnDelay = 0.8f;

    //Passed to colin on instantiation, to set shrink speed.
    private const float shrinkSpeed = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //Allow objects to spawn.
        m_canSpawnNextColin = true;
        
    }

    private bool m_canSpawnNextColin = true;

    // Update is called once per frame
    void Update()
    {
        if (m_canSpawnNextColin)
        {
            StartCoroutine(Spawn());
        }
    }

    //Spawns colin
    IEnumerator Spawn()
    {
        m_canSpawnNextColin = false;

        //Generate random coordinates for spawning
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);

        //Shrink speed is based on Time.time
        if (Time.time != 0)
        {   
            //Decrease the spawnDelay slightly.
            //Debug.Log(GameObject.FindGameObjectsWithTag("Colin").Length);
            if (GameObject.FindGameObjectsWithTag("Colin").Length < 3)
            {
                //Instance, and initialize it.
                Colin newColin = Instantiate(m_colinPrefab, pos, Quaternion.identity).GetComponent<Colin>();
                newColin.m_shrinkSpeed = shrinkSpeed;

                if (m_spawnDelay > 0.35f) { m_spawnDelay -= 0.03f; }
            }
        }
        yield return new WaitForSeconds(m_spawnDelay);
        m_canSpawnNextColin = true;
    }

    public void IncrementScore()
    {
        score++;
        GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text = "SCORE: " + score;
    }
}
