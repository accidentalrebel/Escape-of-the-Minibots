using UnityEngine;
using System.Collections;

public class TriggerableHazard : TriggerableBlocks {

    override protected void Awake()
    {
        base.Awake();
        _prefabToSpawn = Registry.prefabHandler.pfHazard;
    }

    override protected void Start()
    {

    }
}
