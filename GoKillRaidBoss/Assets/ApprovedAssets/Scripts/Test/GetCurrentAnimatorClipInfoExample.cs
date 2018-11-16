using UnityEngine;

public class GetCurrentAnimatorClipInfoExample : MonoBehaviour {
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;

    float m_CurrentClipLength;

    void Update() {
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
    }

    //animator.GetCurrentAnimatorClipInfo(0).clip.name;

    void OnGUI() {
        //Output the current Animation name and length to the screen
        GUI.Label(new Rect(0, 0, 400, 40), "Clip Name : " + m_ClipName);
        GUI.Label(new Rect(0, 30, 400, 40), "Clip Length : " + m_CurrentClipLength);
    }
}