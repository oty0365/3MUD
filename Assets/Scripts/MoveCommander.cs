using UnityEngine;

public class MoveCommander : MonoBehaviour
{
    [SerializeField] private PlayerMove playerController;
    public void JumpAction()
    {
        playerController.Jump();
    }
}
