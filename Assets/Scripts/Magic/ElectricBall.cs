using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class ElectricBall : Magic
{
    public UnityEvent destroy;
    private static List<GameObject> allElectricBalls;
    public GameObject electricChainToStart;
    [SerializeField] protected GameObject electricalChainPrefab;

    [SerializeField] [Tooltip("Через сколько электрический шар исчезнет")]
    protected float lifeTime;

    [SerializeField] [Tooltip("На сколько будет понижаться скорость каждый кадр")]
    private float speedReduction;

    protected override void InitializeElements()
    {
        base.InitializeElements();
        allElectricBalls ??= new List<GameObject>();
        SpawnElectricalChain();
        allElectricBalls.Add(gameObject);
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
        if (allElectricBalls.Count >= 2)
        {
            electricChainToStart = Instantiate(electricalChainPrefab);
            electricChainToStart.GetComponent<ElectricalChain>().SetPosition(gameObject, allElectricBalls[0]);
            
            GameObject lastElectricBall = allElectricBalls[^1];
            GameObject electricalChainToEnd = Instantiate(electricalChainPrefab);
            electricalChainToEnd.GetComponent<ElectricalChain>().SetPosition(gameObject, lastElectricBall);
            Destroy(lastElectricBall.GetComponent<ElectricBall>().electricChainToStart);
        }

        if (allElectricBalls.Count == 1)
        {
            GameObject newElectricalChain = Instantiate(electricalChainPrefab);
            newElectricalChain.GetComponent<ElectricalChain>().SetPosition(gameObject, allElectricBalls[0]);
        }
    }

    public override void Disappear()
    {
        allElectricBalls.Remove(gameObject);
        destroy.Invoke();
        base.Disappear();
    }
    
    protected override void OnCollisionWithObstacle()
    {
        CurrentSpeed = 0;
    }
}