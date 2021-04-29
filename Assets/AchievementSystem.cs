using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : Singleton<AchievementSystem>
{
    Dictionary<string, bool> achievements = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void achieve(string name)
    {
        achievements[name] = true;
    }

    public bool isAchieved(string name)
    {
        return achievements.ContainsKey(name) && achievements[name];
    }
}
