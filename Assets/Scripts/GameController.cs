using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private TextMeshProUGUI startCounter;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject topCorner1;
    [SerializeField] private GameObject topCorner2;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject bottomCorner1;
    [SerializeField] private GameObject bottomCorner2;
    [SerializeField] private Button pauseButton;
    private int counter = 3;
    private bool gameBegin = false;
    private float delay = 1;
    private bool pause = false;

    private void Start()
    {
        StartCoroutine(Counter());
    }

    private void OnEnable()
    {
        Player.onLoose += StopCounter;
        Menu.onPause += Pause;
    }
    private void OnDisable()
    {
        Player.onLoose -= StopCounter;
        Menu.onPause -= Pause;
    }

    private IEnumerator Counter()
    {
        if (!gameBegin)
        {
            startCounter.text = counter.ToString(); //����� � �������� ��������
        }
        if (!pause)
        {
            counter--;
        }
        yield return new WaitForSeconds(delay);

        if(counter <= 0 && !gameBegin)
        {
            pauseButton.interactable = true; //������������ ������ �����
            panel.SetActive(false); //����������� ������
            Instantiate(ball); //���������� ���
            gameBegin = true; //���� ��������
        }

        if (gameBegin && !pause)
        {
            if(delay > 0.25f) //����� �� 1 ��� ���������� ����������� � 1 ������� �� �������� �������
            {
                delay -= 0.0075f;
            }
            if (counter <= -25) //��� � 25 ����� ���������� ��� ���� ���
            {
                Instantiate(ball);
                counter = 0;
            }

            MoveHorizontalWalls(); //������� � ������ ����� ��������� � ������
        }

        StartCoroutine(Counter());
    }

    private void MoveHorizontalWalls()
    {
        topWall.transform.position = new Vector2(topWall.transform.position.x, topWall.transform.position.y - 0.015f);
        topCorner1.transform.position = new Vector2(topCorner1.transform.position.x, topCorner1.transform.position.y - 0.015f);
        topCorner2.transform.position = new Vector2(topCorner2.transform.position.x, topCorner2.transform.position.y - 0.015f);

        bottomWall.transform.position = new Vector2(bottomWall.transform.position.x, bottomWall.transform.position.y + 0.015f);
        bottomCorner1.transform.position = new Vector2(bottomCorner1.transform.position.x, bottomCorner1.transform.position.y + 0.015f);
        bottomCorner2.transform.position = new Vector2(bottomCorner2.transform.position.x, bottomCorner2.transform.position.y + 0.015f);
    }

    private void StopCounter(string name) //����� ���� �� ������� ��������, ������� ���������������
    {
        StopAllCoroutines();
    }

    private void Pause(bool pause)
    {
        this.pause = pause;
    }
}
