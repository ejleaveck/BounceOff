using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
