using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float floatHeight = 1f;
    public float floatDuration = 3f;

    private Vector3 startPos;
    private bool goingUp = true;
    private Coroutine floatCoroutine;

    void Start()
    {
        startPos = transform.position;
        floatCoroutine = StartCoroutine(FloatUpDown());
    }

    public void SetFloatingActive(bool active)
    {
        if (active)
        {
            if (floatCoroutine == null)
                floatCoroutine = StartCoroutine(FloatUpDown());
        }
        else
        {
            if (floatCoroutine != null)
            {
                StopCoroutine(floatCoroutine);
                floatCoroutine = null;
            }
        }
    }

    System.Collections.IEnumerator FloatUpDown()
    {
        while (true)
        {
            Vector3 fromPos = transform.position;
            Vector3 toPos = goingUp ? startPos + new Vector3(0f, floatHeight, 0f) : startPos;
            float elapsed = 0f;

            while (elapsed < floatDuration)
            {
                transform.position = Vector3.Lerp(fromPos, toPos, elapsed / floatDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = toPos;
            goingUp = !goingUp;

            yield return null;
        }
    }
}
