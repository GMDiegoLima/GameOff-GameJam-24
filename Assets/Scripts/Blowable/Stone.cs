using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour, IBlowable
{
    public void GetBlow(Vector2 dir)
    {
        Debug.Log("Stone get blow");
        StartCoroutine(GetBlowRoutine(dir));
	}

    IEnumerator GetBlowRoutine(Vector2 dir)
    { 
        Vector3 endPos = transform.position + (Vector3)dir * 3f;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, 3f * Time.deltaTime);
            yield return new WaitWhile(() => transform.position == endPos);
        }
    }
}
