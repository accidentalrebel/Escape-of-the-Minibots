using UnityEngine;
using System.Collections;

public class GameGUI : GUILayout {

    Main main;
    TextMesh txtMinibotCount;
    TextMesh txtTimer;
        
    void Start()
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
    }

    void ListenerMinibotExit()
    {
        Debug.LogWarning("LISTENER CALLED");
    }
}
