﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVisualController : MonoBehaviour
{
    public Animator m_animator;
    public SpriteRenderer m_sRender;
    public AnimationTriggers m_animTriggers;
    
    [System.Serializable]
    public struct AnimationTriggers
    {
        public string m_upAnim;
        public string m_downAnim;
        public string m_sideAnim;
        public string m_walkingAnimBool;
    }
    private bool m_flipped;

    public void RotateSprite(Vector3 p_dir)
    {
        ResetTriggers();
        if (Mathf.Abs(p_dir.x) > 0)
        {
            m_animator.SetBool(m_animTriggers.m_sideAnim, true);
            if (Mathf.Sign(p_dir.x) > 0)
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
            m_animator.SetBool(m_animTriggers.m_downAnim, true);
        }
    }
    public void ChangeWalkingState(bool p_activeState)
    {
        m_animator.SetBool(m_animTriggers.m_walkingAnimBool, p_activeState);
    }

    private void ResetTriggers()
    {
        m_animator.SetBool(m_animTriggers.m_sideAnim, false);
        m_animator.SetBool(m_animTriggers.m_upAnim, false);
        m_animator.SetBool(m_animTriggers.m_downAnim, false);
        m_sRender.flipX = false;
    }
}