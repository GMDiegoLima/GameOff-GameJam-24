using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{
    Animator animator;
    Collider2D collider;
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    public void OpenBridge()
    {
        animator.SetBool("Open", true);
        StartCoroutine(DisableCollider());
    }
    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
    }
}
