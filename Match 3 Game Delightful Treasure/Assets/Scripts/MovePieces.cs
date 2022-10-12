using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePieces : MonoBehaviour
{

    public static MovePieces instance;
    MainGame game;

    NodePiece moving;
    Point newIndex;
    Vector2 touchStart;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<MainGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving != null)
        {
            Vector2 dir = ((Vector2)Input.mousePosition - touchStart);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            newIndex = Point.clone(moving.index);
            Point add = Point.zero;
            if(dir.magnitude > 32) //если касание дальше изначального положения курсора на 32 пикселя
            {
                // В зависимости от направления делать прибавление к Point
               if(aDir.x > aDir.y)
                    add = (new Point((nDir.x > 0) ? 1 : -1, 0));
               else if (aDir.y > aDir.x)
                    add = (new Point(0, (nDir.y > 0) ? -1 : 1));
            }
            newIndex.add(add);

            Vector2 pos = game.getPositionFromPoint(moving.index);
            if (!newIndex.Equals(moving.index))
                pos += Point.mult(new Point(add.x, -add.y), 16).ToVector();
            moving.MovePositionTo(pos);
        }
    }
    public void MovePiece(NodePiece piece)
    {
        if (moving != null) return;
        SFXManager.instance.PlaySFX(Clip.Select);
        moving = piece;
        touchStart = Input.mousePosition;
    }

    public void DropPiece()
    {
        if (moving == null) return;
        SFXManager.instance.PlaySFX(Clip.Swap);
        if (!newIndex.Equals(moving.index))
            game.FlipPieces(moving.index, newIndex, true); //перевернуть объекты вокруг друг друга, если совпадений не нашлось
        else
            game.ResetPiece(moving);
        moving = null;
    }
}
