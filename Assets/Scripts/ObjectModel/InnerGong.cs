using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerGong
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DefaultEffect { get; set; }
    public string FirstEffect { get; set; }
    public string SixthEffect { get; set; }
    public string TenthEffect { get; set; }
    public int PerHPGain { get; set; }
    public int PerMPGain { get; set; }
    public List<Talent> PerTalentGain { get; set; }
    public int FullHPGain { get; set; }
    public int FullMPGain { get; set; }
    public List<Talent> FullTalentGain { get; set; }
    public int FirstMaxProficiency { get; set; }
    public int NextMaxRatio { get; set; }
}

public enum Talent { Bi, Gen, Wu, Shen, Jing}
