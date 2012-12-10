using UnityEngine;
using System.Collections;

public class GameGUI : GUILayout {

    Main main;
    public TextMesh txtMinibotCount;
    public TextMesh txtTimer;
        
    void Awake()
    {
        main = Registry.main;
        if (main == null)
            Debug.LogError("main is not found!");

        if (txtMinibotCount == null)
            Debug.LogError("txtMinibotCount was not specified!");

        if (txtTimer == null)
            Debug.LogError("txttimer was not specified!");

        Registry.eventDispatcher.EUpdateMinibotCount += LMinibotExit;
        Registry.eventDispatcher.EUpdateTimer += LTimerUpdate;
    }

    void LMinibotExit(string minibotCount)
    {
        txtMinibotCount.text = "Minibots left: " + minibotCount;
    }

    void LTimerUpdate(string time)
    {
        txtTimer.text = time;
    }
}
