using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerCharacterController : MonoBehaviour {

    public string text;
    public string[] characters;
    public GameObject[] textObjects;
    public int currentCharacter;
    public int currentLine;
    public CharacterAnimation[] textAnim;
    public CharacterAnimationLength[] animationLength;
    public CharacterDelay[] charDelay;
    public CharacterScale[] scale;
    public CharacterIdleAnimation[] idle;
    public CharacterIdleLength[] idleLength;
    public GameObject[] characterObjects;
    public string[] database;
    public Vector2 characterPosition;
    public float time;
    public int[] textPerLine;
    public Vector2[] nextLine;
    public Color[] rainbow;
    public CharacterBaseColor[] colors;

	// Use this for initialization
	void Start () {
        Time.timeScale = time;
    }

    public void ShowText()
    {
        SplitText();
        StartCoroutine(Instantiator());
    }
	
    [ContextMenu("SetArray")]
    public void SetArray()
    {
        characters = new string[text.Length];
        textObjects = new GameObject[text.Length];
        textAnim = new CharacterAnimation[text.Length];
        animationLength = new CharacterAnimationLength[text.Length];
        charDelay = new CharacterDelay[text.Length];
        scale = new CharacterScale[text.Length];
        idle = new CharacterIdleAnimation[text.Length];
        idleLength = new CharacterIdleLength[text.Length];
        colors = new CharacterBaseColor[text.Length];
    }

    [ContextMenu("Split Text")]
    public void SplitText()
    {
        for (int a = 0; a < text.Length; a++)
        {
            characters[a] = System.Convert.ToString(text[a]);
        }
        for (int b = 0; b < characters.Length; b++)
        {
            for (int c = 0; c < database.Length; c++)
            {
                if (characters[b] == database[c])
                {
                    textObjects[b] = characterObjects[c];
                }
            }
        }
        for(int c = 0; c < text.Length; c++)
        {
            textAnim[c].name = characters[c];
            animationLength[c].name = characters[c];
            charDelay[c].name = characters[c];
            scale[c].name = characters[c];
            idle[c].name = characters[c];
            idleLength[c].name = characters[c];
            colors[c].name = characters[c];
        }
    }

    [ContextMenu("Default")]
    public void Default()
    {
        for(int c = 0; c < text.Length; c++)
        {
            textAnim[c].anim = animNames.None;
            animationLength[c].length = 0.2f;
            charDelay[c].delay = 0.1f;
            scale[c].scale = 0.1f;
            idle[c].idle = idleNames.None;
            idleLength[c].idleLength = 0.05f;
            colors[c].color = new Color(0, 0, 0, 1);
        }
    }

    public IEnumerator Instantiator()
    {
        for (int d = 0; d < textObjects.Length; d++)
        {
            if (d != 0)
            {
                characterPosition = new Vector2(characterPosition.x + (textObjects[d - 1].GetComponent<Character>().size * scale[d].scale) + scale[d].scale, characterPosition.y);
            }
            else
            {
                characterPosition = new Vector2(characterPosition.x, characterPosition.y);
            }
            if(currentCharacter >= textPerLine[currentLine])
            {
                characterPosition = nextLine[currentLine];
                currentLine += 1;
                currentCharacter = 0;
            }
            GameObject currentChar = Instantiate(textObjects[d], characterPosition, Quaternion.identity);
            currentChar.transform.parent = gameObject.transform;
            currentChar.transform.localScale = new Vector2(scale[d].scale, scale[d].scale);
            currentChar.GetComponent<Character>().startColor = colors[d].color;
            currentChar.GetComponent<Character>().animationLength = animationLength[d].length;
            currentChar.GetComponent<Character>().idleLength = idleLength[d].idleLength;
            if (textObjects[d] != characterObjects[53])
            {
                if(textAnim[d].anim == animNames.Fade)
                {
                    currentChar.GetComponent<Character>().Fade();
                }
                if(textAnim[d].anim == animNames.LeftToRight)
                {
                    currentChar.GetComponent<Character>().LeftToRight();
                }
                if (textAnim[d].anim == animNames.YTilt)
                {
                    currentChar.GetComponent<Character>().YTilt();
                }
                if (textAnim[d].anim == animNames.Puzzle)
                {
                    currentChar.GetComponent<Character>().Puzzle();
                }
                if (textAnim[d].anim == animNames.Scale)
                {
                    currentChar.GetComponent<Character>().Scale();
                }
                if(idle[d].idle == idleNames.Rainbow)
                {
                    currentChar.GetComponent<Character>().idle = 1;
                    currentChar.GetComponent<Character>().rainbow = rainbow;
                }
                if (idle[d].idle == idleNames.Bob)
                {
                    currentChar.GetComponent<Character>().idle = 2;
                }
            }
            currentCharacter += 1;
            yield return new WaitForSeconds(charDelay[d].delay);
        }
    }
}

[System.Serializable]
public class CharacterAnimation
{
    public string name;
    public animNames anim;
}

public enum animNames
{
    None,
    Fade,
    LeftToRight,
    YTilt,
    Puzzle,
    Scale
}

[System.Serializable]
public class CharacterDelay
{
    public string name;
    [Range(0f, 1f)]
    public float delay = 0.1f;
}

[System.Serializable]
public class CharacterScale
{
    public string name;
    [Range(0f, 1f)]
    public float scale = 0.2f;
}

[System.Serializable]
public class CharacterAnimationLength
{
    public string name;
    [Range(0f, 1f)]
    public float length = 0.2f;
}

[System.Serializable]
public class CharacterIdleAnimation
{
    public string name;
    public idleNames idle;
}

public enum idleNames
{
    None,
    Rainbow,
    Bob
}

[System.Serializable]
public class CharacterIdleLength
{
    public string name;
    [Range(0f,1f)]
    public float idleLength = 0.5f;
}

[System.Serializable]
public class CharacterBaseColor
{
    public string name;
    public Color color = new Color(0,0,0,1);
}


