using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStyleEffect
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Detail { get; set; }
    public int Value { get; set; }
    public int TimeValue { get; set; }
    public EffectType Type { get; set; }
}

public enum EffectType { DeBuff, Buff }
