using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public override void Init()
    {
        shape = GameBase.Shapes.square;
        isSelectable = true;
    }
    public override void DoAction(Shape shape)
    {
        Shape selected_shape = GameBase.instance.selected_shape;
        //Для квадрата триангл только
        if (selected_shape.shape == GameBase.Shapes.triangle && GameBase.instance.energy>0)
        {
            size--;
            GameBase.instance.UseEnergy();
            ChangeSizeShape();
            selected_shape.isDisabled = true;
            selected_shape.gameObject.SetActive(false);
        }
    }
    public void ChangeSizeShape()
    {
        transform.localScale = Vector3.one * size / 2;
    }
}
