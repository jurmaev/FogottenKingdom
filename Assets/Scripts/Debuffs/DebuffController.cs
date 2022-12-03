using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DebuffController : MonoBehaviour
{
    [SerializeField] private List<GameObject> allDebuffs;
    private Dictionary<(string, string), string> debuffFormulas;

    private void Start()
    {
        debuffFormulas = new Dictionary<(string, string), string>()
        {
            {("WetDebuff", "ChilledDebuff"), "FrozenDebuff"},
            {("WetDebuff", "FrozenDebuff"), "FrozenDebuff"},
            {("ChilledDebuff", "FrozenDebuff"), "FrozenDebuff"},
            {("WetDebuff", "ElectrifiedDebuff"), "ElectrifiedDebuff"}
        };
    }

    /// <summary>
    /// Если переданные дебаффы можно соединить, то возращает новый дебафф; если один из дебаффов null, то возвращает тот, что не null;
    /// если переданные дебаффы не null и их нельзя соединить, то возращает null
    /// </summary>
    public bool TryMixDebuffs(GameObject firstDebuff, GameObject secondDebuff, out GameObject mixDebuff)
    {
        if (firstDebuff == null && secondDebuff == null)
        {
            mixDebuff = null;
            return false;
        }

        if (firstDebuff != null && secondDebuff == null)
        {
            mixDebuff = Instantiate(firstDebuff);
            return true;
        }

        if (secondDebuff != null && firstDebuff == null)
        {
            mixDebuff = Instantiate(secondDebuff);
            return true;
        }

        if (firstDebuff.GetComponent<Debuff>().DebuffName == secondDebuff.GetComponent<Debuff>().DebuffName)
        {
            mixDebuff = Instantiate(firstDebuff);
            return true;
        }
        
        if (debuffFormulas.TryGetValue((firstDebuff.GetComponent<Debuff>().DebuffName, secondDebuff.GetComponent<Debuff>().DebuffName), out string newDebuffName1))
        {
            mixDebuff = GetDebuffByName(newDebuffName1);
            return true;
        }

        if (debuffFormulas.TryGetValue((secondDebuff.GetComponent<Debuff>().DebuffName, firstDebuff.GetComponent<Debuff>().DebuffName), out string newDebuffName2))
        {
            mixDebuff = GetDebuffByName(newDebuffName2);
            return true;
        }

        mixDebuff = null;
        return false;
    }

    private GameObject GetDebuffByName(string debuffName)
    {
        var debuffPrefub = allDebuffs.FirstOrDefault(debuff => debuff.name == debuffName);
        var debuff = Instantiate(debuffPrefub);
        return debuff;
    }
}