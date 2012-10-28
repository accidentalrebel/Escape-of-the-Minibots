using UnityEngine;
using System.Collections;

public class TriggerableHazard : TriggerableBlocks {

    virtual protected void Awake()
    {
        base.Awake();
        prefabToSpawn = Registry.prefabHandler.pfHazard;
    }

    protected override void Start()
    {

    }
}
