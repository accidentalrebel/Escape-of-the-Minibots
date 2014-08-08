using UnityEngine;
using System.Collections;

public class HorizontalSwitch : Switch {

    void LateUpdate()
    {
        if (_isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            { 
				Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXHorizontalSwitch);
                _triggeredCollider.gameObject.GetComponent<MinibotController>().InvertHorizontally();
            }
        }
    }
}
