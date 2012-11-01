using UnityEngine;
using System.Collections;

public class GameGUI : GUILayout {

    Main main;
    TextMesh txtMinibotCount;
    TextMesh txtTimer;
        
    void Awake()
    {
        main = Registry.main;
        if (main == null)
            Debug.LogError("main is not found!");

        txtMinibotCount = gameObject.transform.FindChild("TextMinibotCount").GetComponent<TextMesh>();
        if (txtMinibotCount == null)
            Debug.LogError("txtMinibotCount is not found!");

        txtTimer = gameObject.transform.FindChild("TextTimer").GetComponent<TextMesh>();
        if (txtTimer == null)
            Debug.LogError("txttimer is not found!");

        Registry.eventDispatcher.UpdateMinibotCount += ListenerMinibotExit;
        Registry.eventDispatcher.UpdateTimer += ListenerTimerUpdate;
    }

    void ListenerMinibotExit(string minibotCount)
    {
        Debug.Log("Minibots left: " + minibotCount);
        txtMinibotCount.text = "Minibots left: " + minibotCount;
    }

    void ListenerTimerUpdate()
    {
        Debug.LogWarning("TIMER UPDATED");
    }
}
