using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public override void Init()
    {
        shape = GameBase.Shapes.circle;
        isSelectable = false;
    }
    public override void DoAction(Shape shape)
    {
        Shape selected_shape = GameBase.instance.selected_shape;
        //Круг встаёт в квадрат
        if (selected_shape.shape == GameBase.Shapes.square)
        {
            if(selected_shape.size < shape.size)
            {
                selected_shape.transform.position = shape.transform.position;
                GameBase.instance.AddMove();
                selected_shape.isDisabled = true;
                shape.isDisabled = true;

            }
        }
    }
}
