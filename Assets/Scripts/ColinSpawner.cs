using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColinSpawner : MonoBehaviour
{
    public int score = 0;

    //Setting difficulty
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty m_difficulty;

    //Colin prefab.
    public GameObject m_colin; 

    //Amount of seconds until next colin is spawned.
    [SerializeField]
    private float m_spawnDelay = 1.0f;

    //Passed to colin on instantiation, to set shrink speed.
    private float shrinkSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Difficulty switches
        switch (m_difficulty)
        {
            case Difficulty.Easy:
                m_spawnDelay = 1;
                shrinkSpeed = .35f;
                break;
            case Difficulty.Medium:
                m_spawnDelay /= 1.5f;
                shrinkSpeed = 0.5f;
                break;
            case Difficulty.Hard:
                m_spawnDelay /= 2;
                shrinkSpeed = 0.5f;
                break;
        }

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
        //Generate random coordinates for spawning
        //200 offset so that it doesnt go offscreen
        int randXCoord = Random.Range(0 + 100, Camera.main.pixelWidth - 100);
        int randYCoord = Random.Range(0 + 100, Camera.main.pixelHeight - 100);
        Vector2 coord = new Vector2(randXCoord, randYCoord);

        Debug.Log("Generated Coordinate: " + coord);

        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);

        //Spawn, wait the delay, and restart.
        m_canSpawnNextColin = false;

        if (Time.time != 0)
        {  
            Instantiate(m_colin, pos, Quaternion.identity).GetComponent<Colin>().m_shrinkSpeed = shrinkSpeed;
        }

        //Decrease the spawnDelay slightly.
        if (m_spawnDelay < 0.25f) { m_spawnDelay -= 0.05f; }
        yield return new WaitForSeconds(m_spawnDelay);
        m_canSpawnNextColin = true;
    }

    public void IncrementScore()
    {
        score++;
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text = "SCORE: " + score;
    }
}
