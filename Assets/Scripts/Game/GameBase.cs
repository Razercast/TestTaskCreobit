using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//�� ����� ��� ��� ������� ������� ������� ��� UI, SceneManagement � ����� ��������� �� ��������� ������.
//�� �� ��� ����� ������. ������� ������� �� � ����� ������
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
        //���� ����� �� ��������
        if(selected_shape==null)
        {
            //�������� ������ �� ��� ����� �.�. �������� ������ �� ��� �����
            if (shape.isSelectable)
            {
                selected_shape = shape;
            }
        } else
        {
            //���� ������ ��� �� ��� �� ��������
            if(selected_shape.shape == shape.shape)
            {
                selected_shape = null;
            } else
            {
                //���� ���� ��������� � �������� ������ �� ��������� ��������
                shape.DoAction(shape);
                selected_shape = null;
                //� �������
                CheckMoves();

            }
        }
    }
    //�������� ����
    public void AddMove()
    {
        moves++;
        UpdateUI();
        //�������� UI
    }

    public void UseEnergy()
    {
        energy--;
        UpdateUI();
    }

    //��������� �������� �� ����
    public void CheckMoves()
    {
        int possibleMoves = 0;
        int activeItems = 0;
        List<Shape> squares = new List<Shape>();
        List<Shape> circles = new List<Shape>();
        List<Shape> triangles = new List<Shape>();
        //���������� ��� ������
        foreach (var item in ShapesList)
        {
            //��������� ������ �� ��� �������� � ������������ � ���� ������
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
        //������ ��������� �����
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
                EndGame("������");
            } else
            {
                EndGame("���������!");
            }
            
        }
    }
    //�������� ��� ������ �� ������
    public void GetShapes()
    {
        ShapesList = Wrapper_shapes.GetComponentsInChildren<Shape>();
    }
    //��������� ������������ �� ������������ �� ������
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
    //��������� �������� �����
    public void UpdateUI()
    {
        txt_moves.text = moves.ToString();
        txt_energy.text = energy.ToString();
    }

    //������� ����� ����� ���� ��� �������/��������
    public void EndGame(string text)
    {
        txt_popup.text = text;
        Popup.SetActive(true);
    }
    #endregion
    #region SceneManagement
    //������� �������� ������
    public void Restart()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
    //����� � ������� �������
    public void LevelSelect()
    {
        SceneManager.LoadScene("LVLSelect");
    }
    #endregion

}
