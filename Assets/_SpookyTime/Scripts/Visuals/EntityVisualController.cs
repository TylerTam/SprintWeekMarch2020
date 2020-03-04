using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityVisualEvent : UnityEngine.Events.UnityEvent { }
public class EntityVisualController : MonoBehaviour
{
    public Animator m_animator;
    public SpriteRenderer m_sRender;
    public AnimationTriggers m_animTriggers;

    public EntityVisualEvent m_finishDigging, m_prematureDig, m_startMoving;
    [System.Serializable]
    public struct AnimationTriggers
    {
        public string m_upAnim;
        public string m_downAnim;
        public string m_sideAnim;
        public string m_walkingAnimBool;
        public string m_diggingBool;
    }
    private bool m_flipped;

    public void RotateSprite(Vector3 p_dir)
    {
        ResetTriggers();
        if (Mathf.Abs(p_dir.x) > 0)
        {
            m_animator.SetBool(m_animTriggers.m_sideAnim, true);
            if (Mathf.Sign(p_dir.x) < 0)
            {
                m_sRender.flipX = false;
            }
            else
            {
                m_sRender.flipX = true;
            }

        }
        else if(Mathf.Abs(p_dir.y) > 0)
        {
            if (Mathf.Sign(p_dir.y) > 0)
            {
                m_animator.SetBool(m_animTriggers.m_upAnim, true);
            }
            else
            {
                m_animator.SetBool(m_animTriggers.m_downAnim, true);
            }
        }
    }
    public void ChangeWalkingState(bool p_activeState)
    {
        if (p_activeState)
        {
            m_startMoving.Invoke();
        }
        m_animator.SetBool(m_animTriggers.m_walkingAnimBool, p_activeState);
    }

    private void ResetTriggers()
    {
        m_animator.SetBool(m_animTriggers.m_sideAnim, false);
        m_animator.SetBool(m_animTriggers.m_upAnim, false);
        m_animator.SetBool(m_animTriggers.m_downAnim, false);
        m_animator.SetBool(m_animTriggers.m_diggingBool, false);
    }

    public void StartDigging()
    {
        m_animator.SetBool(m_animTriggers.m_diggingBool, true);
    }

    public void FinishPrematureDig()
    {
        m_prematureDig.Invoke();
    }

    public void FinishDigging()
    {
        
        m_animator.SetBool(m_animTriggers.m_diggingBool, false);
        m_finishDigging.Invoke();
    }
}
