using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public int experience, coins, hitpoints;

    void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Change(ref int v, int a) => v = Mathf.Max(0, v + a);

    public void AddExperience(int a) => Change(ref experience, a);
    public void AddCoins(int a) => Change(ref coins, a);
    public void AddHitpoints(int a) => Change(ref hitpoints, a);

    void OnGUI()
    {
        string[] names = { "XP", "Coins", "HP" };
        int[] values = { experience, coins, hitpoints };

        for (int i = 0; i < 3; i++)
        {
            GUI.Label(new Rect(10, 10 + i * 20, 200, 20), names[i] + ": " + values[i]);
            if (GUI.Button(new Rect(220, 10 + i * 25, 100, 20), "+10")) Change(ref GetRef(i), 10);
            if (GUI.Button(new Rect(330, 10 + i * 25, 100, 20), "-10")) Change(ref GetRef(i), -10);
        }
    }

    ref int GetRef(int i)
    {
        if (i == 0) return ref experience;
        if (i == 1) return ref coins;
        return ref hitpoints;
    }
}
