using UnityEngine;

public class MoveCommander : MonoBehaviour
{
    [SerializeField] private PlayerMove playerController;
    public void JumpAction()
    {
        GameFlowManager.Instance.HasStarted = true;
        playerController.Jump();
    }
    public void SlideAction(bool afterClicked)
    {
        if (afterClicked)
        {
            playerController.EndSlide();
        }
        else
        {
            playerController.StartSlide();
        }

    }
}
