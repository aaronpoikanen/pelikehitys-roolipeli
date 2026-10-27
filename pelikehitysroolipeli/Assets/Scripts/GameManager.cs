using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        Debug.Log("STATE: " + currentState);

        
        if (currentState == GameState.Paused || currentState == GameState.MainMenu)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void OnGUI()
    {
        
        if (GUI.Button(new Rect(10, 10, 150, 40), "Main Menu"))
        {
            ChangeState(GameState.MainMenu);
        }

       
        if (GUI.Button(new Rect(10, 60, 150, 40), "Play"))
        {
            ChangeState(GameState.Playing);
        }

        
        if (GUI.Button(new Rect(10, 110, 150, 40), "Pause"))
        {
            ChangeState(GameState.Paused);
        }
    }
}