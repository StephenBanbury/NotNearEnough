using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    /// <summary>
    ///   React to a button click event.  Used in the UI Button action definition.
    /// </summary>
    /// <param name="button"></param>
    public void OnButtonClicked(Button button)
    {
        // which GameObject?
        GameObject go = GameObject.Find("RoomController");
        if (go != null)
        {
            AgoraController agoraController = go.GetComponent<AgoraController>();
            if (agoraController == null)
            {
                Debug.LogError("Missing game controller...");
                return;
            }
            if (button.name == "JoinButton")
            {
                agoraController.OnJoinButtonClicked();
            }
            else if (button.name == "LeaveButton")
            {
                agoraController.OnLeaveButtonClicked();
            }
        }
    }
}
