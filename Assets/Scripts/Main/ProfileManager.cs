using UnityEngine;
using System.Collections;

public class ProfileManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        if (Registry.main.settings != null)
        {
            Debug.Log("Saving profile");
            PlayerPrefs.SetString("profileName", Registry.main.currentUser);            
        }
        Debug.Log("The profile name is " + PlayerPrefs.GetString("profileName"));
	}
}
