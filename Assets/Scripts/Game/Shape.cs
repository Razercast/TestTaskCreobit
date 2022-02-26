using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int size = 1;
    public GameBase.Shapes shape { get; set; }
    //ћожно выбрать первым
    public bool isSelectable { get; set; }
    //ѕосле того как кубик оказалс€ в шарике он перестает выбиратьс€
    public bool isDisabled { get; set; }

    private void Start()
    {
        transform.localScale = Vector3.one * size/2;
        Init();
        
    }
    public virtual void Init()
    {
        
    }

    public void OnMouseDown()
    {
        if (isDisabled == false)
        {
            Pick();
        }
    }
    private void Pick()
    {
        GameBase.instance.Pick(this);
    }
    public virtual void DoAction(Shape shape)
    {

    }
}
