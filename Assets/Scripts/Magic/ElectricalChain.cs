using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class ElectricalChain : Magic
{
    public void SetPosition(GameObject startObject, GameObject endObject)
    {
        var lightningEffect = gameObject.GetComponent<LightningBoltScript>();
        lightningEffect.StartObject = startObject;
        lightningEffect.EndObject = endObject;
        startObject.GetComponent<ElectricBall>().destroy.AddListener(Disappear);
        endObject.GetComponent<ElectricBall>().destroy.AddListener(Disappear);
    }
}