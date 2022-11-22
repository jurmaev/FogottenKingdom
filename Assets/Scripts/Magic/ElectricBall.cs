using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class ElectricBall : Magic
{
    public UnityEvent destroy;
    private static GameObject lastElectricBall;
    private GameObject previousElectricBall;
    [SerializeField] protected GameObject electricalChainPrefab;

    [SerializeField] [Tooltip("Через сколько электрический шар исчезнет")]
    protected float lifeTime;

    [SerializeField] [Tooltip("На сколько будет понижаться скорость каждый кадр")]
    private float speedReduction;

    protected override void InitializeElements()
    {
        base.InitializeElements();
        SpawnElectricalChain();
        if (lastElectricBall != null)
            previousElectricBall = lastElectricBall;
        lastElectricBall = this.gameObject;
        Invoke(nameof(Disappear), lifeTime);
    }

    protected override void MoveForward()
    {
        base.MoveForward();
        if (CurrentSpeed - speedReduction < 0)
            CurrentSpeed = 0;
        else
            CurrentSpeed -= speedReduction;
    }
    
    
    private void SpawnElectricalChain()
    {
        if (lastElectricBall != null)
        {
            GameObject newElectricalChain = Instantiate(electricalChainPrefab);
            newElectricalChain.GetComponent<ElectricalChain>().SetPosition(gameObject, lastElectricBall);
        }
    }

    public override void Disappear()
    {
        if (lastElectricBall == gameObject)
            lastElectricBall = previousElectricBall;
        destroy.Invoke();
        base.Disappear();
    }
}