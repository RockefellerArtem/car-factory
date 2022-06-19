using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Keeper
{
    public string Name;
    public int Gasoline;
    public int Doors;
    public int Power;
    public int SO;
    public int ColorNumber;
}

[System.Serializable]
public class KeeperSaver
{
    public Keeper[] Keepers;
}
