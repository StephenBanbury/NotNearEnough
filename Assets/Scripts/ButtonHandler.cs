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
            AgoraController roomController = go.GetComponent<AgoraController>();
            if (roomController == null)
            {
                Debug.LogError("Missing game controller...");
                return;
            }
            if (button.name == "JoinButton")
            {
                roomController.OnJoinButtonClicked();
            }
            else if (button.name == "LeaveButton")
            {
                roomController.OnLeaveButtonClicked();
            }
        }
    }
}
