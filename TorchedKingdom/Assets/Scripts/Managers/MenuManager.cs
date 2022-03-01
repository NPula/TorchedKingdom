using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] Image m_imageToFade;

    private static MenuManager m_instance;
    public static MenuManager Instance { get { return m_instance; } }

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    public void FadeImage()
    {
        Animator animator = m_imageToFade.GetComponent<Animator>();
        animator.SetTrigger("startFade");
    }
}
