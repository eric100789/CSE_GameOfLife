using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonnaDestroy : MonoBehaviour
{    
    private float ftime = 0f;
    private SpriteRenderer m_spriteRenderer;

    void Start() 
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        ftime += Time.deltaTime;
        if(ftime >= 2f)
        {
            ftime = 0f;
            Destroy(this.gameObject);
        }

        if(m_spriteRenderer.color.a > 0f) 
        {
            m_spriteRenderer.color = new Color(m_spriteRenderer.color.r , m_spriteRenderer.color.g , m_spriteRenderer.color.b , m_spriteRenderer.color.a - Time.deltaTime*0.5f);
        }
    }
}
