using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerColiderOffsets
{
    public PlayerBehave key;
    public Vector2 pos;
    public Vector2 size;
}

public class PlayerInteraction : HalfSingleMono<PlayerInteraction>
{
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private PlayerColiderOffsets[] playerColiders;
    public Dictionary<PlayerBehave, (Vector2, Vector2)> moveSetColiders = new();
    void Start()
    {
        foreach(var i in playerColiders)
        {
            moveSetColiders.Add(i.key, (i.pos, i.size));
        }
    }

    void Update()
    {
        
    }
    public void ChangeHitBox(PlayerBehave behave)
    {
        boxCol.size = moveSetColiders[behave].Item2;
        boxCol.offset = moveSetColiders[behave].Item1;
    }

}
