using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<float> GetCurrentGameTime;

    private float gameTime = .02f;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
      if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
      else if (Instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
       
    }


    private void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;

        NotifyCurrentGameTime();
    }

    private void NotifyCurrentGameTime()
    {
 GetCurrentGameTime?.Invoke(gameTime);
    }
}
