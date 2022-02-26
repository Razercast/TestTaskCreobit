using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{
    public override void Init()
    {
        shape = GameBase.Shapes.triangle;
        isSelectable = true;
    }

    public override void DoAction(Shape shape)
    {
        base.DoAction(shape);
    }
}
