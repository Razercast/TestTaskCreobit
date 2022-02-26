using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Да можно вот эти регионы которые пометил как UI, SceneManagement в конце разделить на отдельные модули.
//Но их там всего ничего. Поэтому оставил всё в одном классе
public class GameBase : MonoBehaviour
{
    public static GameBase instance;
    public Text txt_moves;
    public Text txt_energy;
    public Text txt_popup;
    public GameObject Popup;
    public GameObject Wrapper_shapes;
    public GameObject EnergyBlock;
    public enum Shapes
    {
        square,
        circle,
        triangle,
    }
    public Shape selected_shape { get; set; }
    public int energy;
    private int moves { get; set; }
    private Shape[] ShapesList;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateUI();
        GetShapes();
        CheckTrianglesMechanics();
    }
    #region GameLogic
    public void Pick(Shape shape)
    {
        //Если пусто то выбираем
        if(selected_shape==null)
        {
            //Выбирать только то что можно т.е. беруться только то что можно
            if (shape.isSelectable)
            {
                selected_shape = shape;
            }
        } else
        {
            //Если выбран тот же тип то отменить
            if(selected_shape.shape == shape.shape)
            {
                selected_shape = null;
            } else
            {
                //Если есть выбранный и выбирают другое то выполнить действие
                shape.DoAction(shape);
                selected_shape = null;
                //И энергия
                CheckMoves();

            }
        }
    }
    //Добавить счет
    public void AddMove()
    {
        moves++;
        UpdateUI();
        //Обновить UI
    }

    public void UseEnergy()
    {
        energy--;
        UpdateUI();
    }

    //Проверять остались ли ходы
    public void CheckMoves()
    {
        int possibleMoves = 0;
        int activeItems = 0;
        List<Shape> squares = new List<Shape>();
        List<Shape> circles = new List<Shape>();
        List<Shape> triangles = new List<Shape>();
        //Перебираем все фигуры
        foreach (var item in ShapesList)
        {
            //Проверяем только те что остались и перекидываем в свой список
            if (item.isDisabled==false)
            {
                if(item.shape == Shapes.square)
                {
                    squares.Add(item);
                } else if(item.shape == Shapes.circle)
                {
                    circles.Add(item);
                } else if(item.shape == Shapes.triangle) {
                    triangles.Add(item);
                }
                activeItems++;
            }
        }

        //Debug.Log(circles);
        //Расчет возможных ходов
        foreach (var square in squares)
        {
            foreach (var circle in circles)
            {
                if (square.size < circle.size)
                {
                    possibleMoves++;
                }
            }
        }
        if (triangles.Count > 0 && squares.Count > 0 && energy>0)
        {
            possibleMoves++;
        }

        if (possibleMoves <= 0)
        {
            if (activeItems == 0)
            {
                EndGame("Победа");
            } else
            {
                EndGame("Поражение!");
            }
            
        }
    }
    //Получить все фигуры на уровне
    public void GetShapes()
    {
        ShapesList = Wrapper_shapes.GetComponentsInChildren<Shape>();
    }
    //Проверить используются ли треугольники на уровне
    public void CheckTrianglesMechanics()
    {
        if (energy > 0)
        {
            Debug.Log(ShapesList);
            foreach (var item in ShapesList)
            {
                if (item.shape == Shapes.triangle)
                {
                    EnergyBlock.SetActive(true);
                }
            }
        }
    }
    #endregion
    #region UI
    //Обновлять значения Ходов
    public void UpdateUI()
    {
        txt_moves.text = moves.ToString();
        txt_energy.text = energy.ToString();
    }

    //Открыть Попап после того как победил/проиграл
    public void EndGame(string text)
    {
        txt_popup.text = text;
        Popup.SetActive(true);
    }
    #endregion
    #region SceneManagement
    //Рестарт текущего уровня
    public void Restart()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
    //Сцена с выбором уровней
    public void LevelSelect()
    {
        SceneManager.LoadScene("LVLSelect");
    }
    #endregion

}
