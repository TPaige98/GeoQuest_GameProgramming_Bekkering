using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject panel;

    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;

    private int two = 2;
    private int three = 3;

    public GameObject questionBlock1;
    public GameObject questionBlock2;
    public GameObject questionBlock3;

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    private int correctAnswers = 0;

    [SerializeField]
    private Text fact;

    [SerializeField]
    private Text score;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        GetRandomQuestion();

        score.text = "Score: " + correctAnswers + "/" + two;
    }

    public void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        fact.text = currentQuestion.question;
    }
    IEnumerator nextQuestion()
    {
        yield return new WaitForSeconds(1.0f);

        GetRandomQuestion();
    }

    IEnumerator correctAnswer()
    {
        fact.text = "Correct, Move Onward!";
        yield return new WaitForSeconds(0.75f);

        panel.SetActive(false);
    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            StartCoroutine(correctAnswer());

            if (correctAnswers == 0)
            {
                wall1.SetActive(false);
                questionBlock1.SetActive(false);
            }
            else if (correctAnswers == 1)
            {
                wall2.SetActive(false);
                questionBlock2.SetActive(false);
            }
            else if (correctAnswers == 2)
            {
                wall3.SetActive(false);
                questionBlock3.SetActive(false);
            }

            correctAnswers++;
            score.text = "Score: " + correctAnswers + "/" + two;
        }
        else
        {
            Debug.Log("Wrong");
            fact.text = "Wrong!, Try Again";
        }

        StartCoroutine(nextQuestion());
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            StartCoroutine(correctAnswer());

            if (correctAnswers == 0)
            {
                wall1.SetActive(false);
                questionBlock1.SetActive(false);
            }
            else if (correctAnswers == 1)
            {
                wall2.SetActive(false);
                questionBlock2.SetActive(false);
            }
            else if (correctAnswers == 2)
            {
                wall3.SetActive(false);
                questionBlock3.SetActive(false);
            }

            correctAnswers++;
            score.text = "Score: " + correctAnswers + "/" + two;
        }
        else
        {
            Debug.Log("Wrong");
            fact.text = "Wrong!, Try Again";
        }

        StartCoroutine(nextQuestion());
    }
}
