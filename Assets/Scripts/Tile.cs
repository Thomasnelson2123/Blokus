using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private SpriteRenderer r;
    [SerializeField] private Sprite[] sprites;

    public enum Color
    {
        None,
        Red,
        Blue,
        Green,
        Yellow
    }

    private Color color;
    public Tile()
    {
        color = Color.None;
        
    }

    public Color GetColor()
    {
        return this.color;
    }

    public void SetColor(Color color)
    {
        this.color = color;
        r.sprite = sprites[(int)color];
    }

}
