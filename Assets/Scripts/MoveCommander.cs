using UnityEngine;

public class MoveCommander : MonoBehaviour
{
    
    [SerializeField] private PlayerMove playerController;
    public void JumpAction()
    {
        GameFlowManager.Instance.SetGame(true);
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
            GameFlowManager.Instance.SetGame(true);
            playerController.StartSlide();
        }

    }
}
