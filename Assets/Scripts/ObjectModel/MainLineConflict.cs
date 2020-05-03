using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLineConflict
{
    public ConflictKind ConflictForm { get; set; }
    public List<Person> ZFriends { get; set; }
    public List<Person> ZEnemys { get; set; }
    public List<Person> FFriends { get; set; }
    public List<Person> FEnemys { get; set; }
    public bool IsZ { get; set; }
    public string Title { get; set; }
}

public enum ConflictKind { Battle, WeiQi, ThanLiquor }