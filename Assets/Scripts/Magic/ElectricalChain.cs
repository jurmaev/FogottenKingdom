using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class ElectricalChain : Magic
{
    private GameObject startObject;
    protected GameObject endObject;


    private void Update()
    {
        UpdateColliderPosition();
    }

    public void SetPosition(GameObject startObject, GameObject endObject)
    {
        this.startObject = startObject;
        this.endObject = endObject;
        
        var lightningEffect = gameObject.GetComponent<LightningBoltScript>();
        lightningEffect.StartObject = startObject;
        lightningEffect.EndObject = endObject;

        startObject.GetComponent<ElectricBall>().destroy.AddListener(Disappear);
        endObject.GetComponent<ElectricBall>().destroy.AddListener(Disappear);

        UpdateColliderPosition();
    }

    private void UpdateColliderPosition()
    {
        var edgeCollider = GetComponent<EdgeCollider2D>();
        var newEdgeColliderPoints = edgeCollider.points;
        newEdgeColliderPoints[0] = transform.InverseTransformPoint(startObject.transform.position);
        newEdgeColliderPoints[1] = transform.InverseTransformPoint(endObject.transform.position);
        edgeCollider.points = newEdgeColliderPoints;
    }

    protected override void OnCollisionWithEnemy(Enemy enemy)
    {
    }

    protected override void OnCollisionWithObstacle()
    {
      
    }
}