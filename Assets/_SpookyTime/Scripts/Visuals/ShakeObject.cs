using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{

    public float m_shakeAmount;
    public float m_shakeTime;
    public GameObject m_shakeObject;
    Vector2 initialPos;
    private Coroutine m_shakeCoroutine;

    [Header("Debugging")]
    public bool m_debuggingTools;
    public Color m_gizmos1, m_gizmos2;

    private void Start()
    {
        
        
    }

    public void StartShaking()
    {

        StopAllCoroutines();
        m_shakeCoroutine = StartCoroutine(ShakeCamera());
        initialPos = transform.localPosition;
    }

    private IEnumerator ShakeCamera()
    {

        float fxTime = 0;
        while (fxTime < m_shakeTime)
        {
            Vector2 newShakePos = Random.insideUnitCircle * Mathf.Lerp(m_shakeAmount, 0, fxTime / m_shakeTime);
            m_shakeObject.transform.localPosition =  newShakePos;
            fxTime += Time.deltaTime;
            yield return null;
        }
        m_shakeObject.transform.localPosition = Vector3.zero;
    }

}