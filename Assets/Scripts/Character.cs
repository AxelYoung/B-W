using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float size;
    public bool turn;
    public float animationLength;
    public float idleLength;
    public Vector2[] startPos;
    public int idle;
    public Color[] rainbow;
    public int currentColor;
    public int bobUp;
    public Vector2[] randStartPos;
    public float t;
    public bool canMove;
    public Color startColor;

    public void Start()
    {
        StartCoroutine(WaitForAnimationFin());
    }

    public IEnumerator WaitForAnimationFin()
    {
        yield return new WaitForSeconds(animationLength);
        if (idle == 1)
        {
            StartCoroutine(RainbowIdle());
        }
        if(idle == 2)
        {
            startPos = new Vector2[gameObject.transform.childCount];
            for (int e = 0; e < gameObject.transform.childCount; e++)
            {
                startPos[e] = transform.GetChild(e).gameObject.transform.localPosition;
            }
            StartCoroutine(BobIdle());
        }
    }

    public void Fade()
    {
        for(int a = 0; a < gameObject.transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, 0);
        }
        StartCoroutine(FadeCoroutine());
    }

    public IEnumerator FadeCoroutine()
    {
        for (int b = 0; b < gameObject.transform.childCount; b++)
        {
            transform.GetChild(b).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(b).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / animationLength);
        }
        if(transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a != 1)
        {
            yield return null;
            StartCoroutine(FadeCoroutine());
        }
    }

    public void LeftToRight()
    {
        for (int c = 0; c < gameObject.transform.childCount; c++)
        {
            transform.GetChild(c).gameObject.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, 0);
        }
        StartCoroutine(LeftToRightCoroutine());
    }

    public IEnumerator LeftToRightCoroutine()
    {
        for (int d = 0; d < gameObject.transform.childCount; d++)
        {
            if(size == 1)
            {
                transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength));
            }
            if(size == 2)
            {
                if(transform.GetChild(d).transform.localPosition.x == -2f)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.25f * animationLength)));
                }
                if (transform.GetChild(d).transform.localPosition.x == -1f)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength));
                }
            }
            if (size == 3)
            {
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == -2)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.5f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == -1)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.25f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == 0)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength));
                }
            }
            if (size == 4)
            {
                if (transform.GetChild(d).transform.localPosition.x == -2)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.75f * animationLength)));
                }
                if (transform.GetChild(d).transform.localPosition.x == -1)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.5f * animationLength)));
                }
                if (transform.GetChild(d).transform.localPosition.x == 0)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.25f * animationLength)));
                }
                if (transform.GetChild(d).transform.localPosition.x == 1)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength));
                }
            }
            if (size == 5)
            {
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == -2)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.999999999f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == -1)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.75f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == 0)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.5f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == 1)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength - (0.25f * animationLength)));
                }
                if (Mathf.Round(transform.GetChild(d).transform.localPosition.x) == 2)
                {
                    transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(d).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), Time.deltaTime / (animationLength));
                }
            }
        }
        yield return null;
        StartCoroutine(LeftToRightCoroutine());
    }

    public void Puzzle()
    {
        canMove = true;
        startPos = new Vector2[gameObject.transform.childCount];
        randStartPos = new Vector2[gameObject.transform.childCount];
        for (int e = 0; e < gameObject.transform.childCount; e++)
        {
            startPos[e] = transform.GetChild(e).gameObject.transform.localPosition;
            transform.GetChild(e).gameObject.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, 0);
            transform.GetChild(e).gameObject.transform.localPosition = new Vector2(transform.GetChild(e).gameObject.transform.localPosition.x + (Random.Range(-5f, 5f)), transform.GetChild(e).gameObject.transform.localPosition.y + (Random.Range(-4f, 4f)));
        }
        StartCoroutine(PieceTogether());
        StartCoroutine(CheckPos());
    }

    public IEnumerator PieceTogether()
    {
        for (int f = 0; f < gameObject.transform.childCount; f++)
        {
            t = 0;
            randStartPos[f] = transform.GetChild(f).transform.localPosition;
            t += Time.deltaTime / (animationLength / 3f);
            transform.GetChild(f).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(f).gameObject.GetComponent<SpriteRenderer>().color, new Color(startColor.r, startColor.g, startColor.b, 1), t * 5);
            transform.GetChild(f).gameObject.transform.localPosition = Vector2.Lerp(randStartPos[f], startPos[f], t);
        }
        yield return null;
        if(canMove)
        StartCoroutine(PieceTogether());
    }

    public IEnumerator CheckPos()
    {
        yield return new WaitForSeconds(animationLength);
        for (int f = 0; f < gameObject.transform.childCount; f++)
        {
            transform.GetChild(f).gameObject.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, 1);
            transform.GetChild(f).gameObject.transform.localPosition = startPos[f];
        }
        canMove = false;
    }

    public void Scale()
    {
        for (int g = 0; g < gameObject.transform.childCount; g++)
        {
            transform.GetChild(g).GetComponent<SpriteRenderer>().color = startColor;
            transform.GetChild(g).gameObject.transform.localScale = new Vector2(0,0);
        }
        StartCoroutine(ScaleUp());
    }

    public IEnumerator ScaleUp()
    {
        for (int h = 0; h < gameObject.transform.childCount; h++)
        {
            transform.GetChild(h).gameObject.transform.localScale = Vector3.Lerp(transform.GetChild(h).gameObject.transform.localScale, new Vector3(1,1,1), Time.deltaTime / animationLength);
        }
        yield return null;
        StartCoroutine(ScaleUp());
    }

    public void YTilt()
    {
        for (int g = 0; g < gameObject.transform.childCount; g++)
        {
            transform.GetChild(g).GetComponent<SpriteRenderer>().color = startColor;
        }
        gameObject.transform.rotation = Quaternion.Euler(0,-90,0);
        turn = true;
    }

    private void Update()
    {
        if (turn)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime / animationLength);
        }
    }

    public IEnumerator RainbowIdle()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color, rainbow[currentColor], Time.deltaTime / idleLength);
        }
        if (transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color == rainbow[currentColor])
        {
            if (currentColor == rainbow.Length - 1)
            {
                currentColor = 0;
            }
            else
            {
                currentColor += 1;
            }
        }
        yield return null;
        StartCoroutine(RainbowIdle());
    }

    public IEnumerator BobIdle()
    {
        for(int j = 0; j < gameObject.transform.childCount; j++)
        {
            if(bobUp == 0)
            {
                transform.GetChild(j).gameObject.transform.localPosition = Vector3.Lerp(transform.GetChild(j).gameObject.transform.localPosition, new Vector3(startPos[j].x, startPos[j].y - 1f), Time.deltaTime / idleLength);
                if(transform.GetChild(j).gameObject.transform.localPosition == new Vector3(startPos[j].x, startPos[j].y - 1f))
                {
                    bobUp = 1;
                }
            }
            if (bobUp == 1)
            {
                transform.GetChild(j).gameObject.transform.localPosition = Vector3.Lerp(transform.GetChild(j).gameObject.transform.localPosition, new Vector3(startPos[j].x, startPos[j].y), Time.deltaTime / idleLength);
                if (transform.GetChild(j).gameObject.transform.localPosition == new Vector3(startPos[j].x, startPos[j].y))
                {
                    bobUp = 0;
                }
            }
        }

        yield return null;
        StartCoroutine(BobIdle());
    }
}
