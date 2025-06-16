using System;
using UnityEngine;

[Serializable]
public struct ColiderBounder
{
    public float upBoard;
    public float downBoard;
    public float leftBoard;
    public float rightBoard;
}

public interface ColiderObj
{
    public void Enter() { }
    public void Stay() { }
    public void Exit() { }

}

public class OtyColider2D : MonoBehaviour,ColiderObj
{
    public ColiderBounder coliderBounder;

    private void Start()
    {
        ColisionManager2D.Instance.coliders.Add(gameObject, (gameObject.transform.position, coliderBounder));
    }

    private void Update()
    {
        ColisionManager2D.Instance.coliders[gameObject] = (gameObject.transform.position, coliderBounder);
    }



    public Rect GetWorldRect()
    {
        Vector2 pos = transform.position;
        float left = pos.x + coliderBounder.leftBoard;
        float right = pos.x + coliderBounder.rightBoard;
        float down = pos.y + coliderBounder.downBoard;
        float up = pos.y + coliderBounder.upBoard;
        return new Rect(new Vector2(left, down), new Vector2(right - left, up - down));
    }

    private void OnDrawGizmos()
    {
        Rect r = GetWorldRect();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(r.center, r.size);
    }
}