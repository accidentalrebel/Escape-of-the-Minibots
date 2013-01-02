using UnityEngine;
using System.Collections;

public class GameGUI : GUILayout {

    Main main;
    Timer timer;
    public TextMesh txtMinibotCount;
    public TextMesh txtTimer;
    public TextMesh txtVersions;
    public TextMesh txtCurrentLevel;
        
    void Awake()
    {
        main = Registry.main;
        if (main == null)
            Debug.LogError("main is not found!");

        if (txtMinibotCount == null)
            Debug.LogError("txtMinibotCount was not specified!");

        if (txtTimer == null)
            Debug.LogError("txttimer was not specified!");

        if (txtVersions == null)
            Debug.LogError("txtVersions was not specified!");

        if (txtCurrentLevel == null)
            Debug.LogError("txtCurrentLevel was not specified!");

        timer = Registry.main.timer;
        timer.ETimerTick += OnTimerTick;

        main.EUpdateMinibotCount += UpdateMinibotCount;
        main.ELevelStarted += LevelStarted;
    }

    void Start()
    {
        txtVersions.text = "Engine: " + Registry.main.engineVersion + " | Map: " + Registry.main.mapPackVersion;
    }

    void OnTimerTick(string currentTime)
    {
        txtTimer.text = currentTime;
    }

    void UpdateMinibotCount()
    {
        txtMinibotCount.text = "Minibots left: " + main.CountMinibotsInLevel().ToString();
    }

    void LevelStarted()
    {
        txtCurrentLevel.text = "Level: " + Registry.map.currentLevel;
    }
}
