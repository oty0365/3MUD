using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private float generateDistance;
    private GameObject _target;
    
    void Start()
    {
        _target = PlayerStatus.Instance.gameObject;
    }
    void Update()
    {
        if (_target.transform.position.x < gameObject.transform.position.x&&Mathf.Abs(_target.transform.position.x- gameObject.transform.position.x)<generateDistance)
        {
            ObjectPooler.Instance.Get(map, gameObject.transform.position, Vector2.zero);
            gameObject.SetActive(false);
        }
    }
}
