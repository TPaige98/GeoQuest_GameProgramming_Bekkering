using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using System.Collections.Specialized;

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

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    public int correctAnswers = 0;

    [SerializeField]
    private Text fact;

    [SerializeField]
    private Text score2;

    [SerializeField]
    private Text score3;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            LoadQuestions("questions");
        }
        GetRandomQuestion();
        score2.text = "Score: " + correctAnswers + "/" + two;
        score3.text = "Score: " + correctAnswers + "/" + three;
    }

    public void LoadQuestions(string fileName)
    {
        if (unansweredQuestions != null)
        {
            unansweredQuestions.Clear();
        }

        Debug.Log("Attempting to Load Questions from: " + fileName);
        TextAsset questions = Resources.Load<TextAsset>(fileName);

        unansweredQuestions = new List<Question>();

        try
        {
            if (questions != null)
            {
                Debug.Log("Questions file loaded!");
                string[] lines = questions.text.Split('\n');

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        Question q = new Question();
                        q.question = parts[0];
                        bool.TryParse(parts[1], out q.isTrue);
                        unansweredQuestions.Add(q);
                    }
                }
            }
            else
            {
                Debug.LogError("File not found: " + fileName);
            }
        }
        catch (UnityException e)
        {
            Debug.LogError("Error Occurred: " + e);
        }
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
        Debug.Log("User selected true");
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
            score2.text = "Score: " + correctAnswers + "/" + two;
            score3.text = "Score: " + correctAnswers + "/" + three;
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
        Debug.Log("User selected false");
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
            score2.text = "Score: " + correctAnswers + "/" + two;
            score3.text = "Score: " + correctAnswers + "/" + three;
        }
        else
        {
            Debug.Log("Wrong");
            fact.text = "Wrong!, Try Again";
        }

        StartCoroutine(nextQuestion());
    }
}
