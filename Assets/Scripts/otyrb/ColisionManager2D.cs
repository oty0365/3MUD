using System.Collections.Generic;
using UnityEngine;

public class ColisionManager2D : MonoBehaviour
{
    public static ColisionManager2D Instance;
    public Dictionary<GameObject, (Vector2, ColiderBounder)> coliders = new();
    public HashSet<(GameObject, GameObject)> currentColisions = new();
    public HashSet<(GameObject, GameObject)> prevColisions = new();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        CheckAABB();

    }

    private void CheckAABB()
    {
        Dictionary<GameObject, ColiderObj> coliderObjs = new();
        foreach (var kvp in coliders)
        {
            if (kvp.Key.TryGetComponent(out ColiderObj obj))
                coliderObjs[kvp.Key] = obj;
        }

        HashSet<(GameObject, GameObject)> checkedPairs = new();

        foreach (var myKvp in coliders)
        {
            GameObject myObj = myKvp.Key;
            Vector2 myPos = myKvp.Value.Item1;
            ColiderBounder myBounder = myKvp.Value.Item2;

            float myLeft = myPos.x + myBounder.leftBoard;
            float myRight = myPos.x + myBounder.rightBoard;
            float myDown = myPos.y + myBounder.downBoard;
            float myUp = myPos.y + myBounder.upBoard;

            foreach (var otherKvp in coliders)
            {
                GameObject otherObj = otherKvp.Key;
                if (myObj == otherObj || checkedPairs.Contains((otherObj, myObj)))
                    continue;

                checkedPairs.Add((myObj, otherObj));

                Vector2 otherPos = otherKvp.Value.Item1;
                ColiderBounder otherBounder = otherKvp.Value.Item2;

                float otherLeft = otherPos.x + otherBounder.leftBoard;
                float otherRight = otherPos.x + otherBounder.rightBoard;
                float otherDown = otherPos.y + otherBounder.downBoard;
                float otherUp = otherPos.y + otherBounder.upBoard;

                bool isOverlapping = !(myRight < otherLeft || myLeft > otherRight || myUp < otherDown || myDown > otherUp);

                bool wasOverlapping = prevColisions.Contains((myObj, otherObj)) || prevColisions.Contains((otherObj, myObj));

                ColiderObj mine = coliderObjs.ContainsKey(myObj) ? coliderObjs[myObj] : null;
                ColiderObj other = coliderObjs.ContainsKey(otherObj) ? coliderObjs[otherObj] : null;

                if (isOverlapping)
                {
                    currentColisions.Add((myObj, otherObj));

                    if (wasOverlapping)
                    {
                        mine?.Stay();
                        other?.Stay();
                    }
                    else
                    {
                        mine?.Enter();
                        other?.Enter();
                    }
                }
                else if (wasOverlapping)
                {
                    mine?.Exit();
                    other?.Exit();
                }
            }
        }

        prevColisions = new HashSet<(GameObject, GameObject)>(currentColisions);
        currentColisions.Clear();
    }

}