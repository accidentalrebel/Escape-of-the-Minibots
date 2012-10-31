using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Main))]
public class GameGUI : MonoBehaviour {

    Main main;

    void Start()
    {
        main = gameObject.GetComponent<Main>();
        if (main == null)
            Debug.LogError("main is not found!");
    }

    void OnGUI()
    {
        GUI.Label(new Rect((Screen.width / 2) - 110, 10, 100, 30), "Minibots Left: " + main.CountMinibotsLeft().ToString());
        GUI.Label(new Rect((Screen.width / 2) + 10, 10, 100, 30), "00:00:00");
    }
}
