using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class TestScript : InputTestFixture
{
    Mouse mouse;
    Keyboard keyboard;

    public override void Setup()
    {
        SceneManager.LoadScene("Level1");
        mouse = InputSystem.AddDevice<Mouse>();
        keyboard = InputSystem.AddDevice<Keyboard>();
    }

    public override void TearDown()
    {
        SceneManager.LoadScene("Level1");

        InputSystem.RemoveDevice(mouse);
        InputSystem.RemoveDevice(keyboard);
    }

    [UnityTest]
    public IEnumerator _1CheckPlayerSpawn()
    {
        GameObject player = GameObject.Find("Player");

        yield return new WaitForSeconds(.1f);

        Assert.That(player, Is.Not.Null);
    }

    [UnityTest]
    public IEnumerator _2CheckPlayerMovement()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        playerMovement.horizontalInput = 1.0f;

        yield return new WaitForSeconds(1.5f);

        Assert.Greater(player.transform.position.x, 0f);
    }

    [UnityTest]
    public IEnumerator _3CheckPlayerJump()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        yield return new WaitForSeconds(1.0f);

        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.5f);
        Release(keyboard.spaceKey);

        Assert.Greater(player.transform.position.y, 0f);
    }

    [UnityTest]
    public IEnumerator _4CheckKillzone()
    {
        GameObject player = GameObject.Find("Player");

        yield return new WaitForSeconds(1.0f);

        player.transform.position = new Vector3(0, -10, 0);

        yield return new WaitForSeconds(1.5f);

        Assert.AreEqual("Level1", SceneManager.GetActiveScene().name, "Scene Did Not Load");
    }

    [UnityTest]
    public IEnumerator _5CheckQuizActivation()
    {
        GameObject player = GameObject.Find("Player");
        GameObject quizBlock = GameObject.Find("QuestionBlock1");

        yield return new WaitForSeconds(1.0f);

        player.transform.position = Vector2.zero;
        quizBlock.transform.position = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        GameObject question = GameObject.Find("Question");

        Assert.IsTrue(question.activeSelf, "question not activated");
    }

    [UnityTest]
    public IEnumerator _6CheckWallDeactivation()
    {
        GameObject player = GameObject.Find("Player");
        GameObject quizBlock = GameObject.Find("QuestionBlock1");
        GameObject checkPoint = GameObject.Find("WallOne");

        yield return new WaitForSeconds(1.0f);

        player.transform.position = quizBlock.transform.position;

        yield return new WaitForSeconds(2.0f);

        GameObject question = GameObject.Find("Question");
        GameObject trueButton = GameObject.Find("True");

        Assert.IsTrue(question.activeSelf, "question not activated");
        Assert.IsTrue(trueButton.activeSelf, "button not activated");

        while (checkPoint.activeSelf)
        {
            trueButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        Assert.IsFalse(checkPoint.activeSelf, "wall still activated");
    }

    [UnityTest]
    public IEnumerator _7CheckScoreIncrease()
    {
        GameObject game = GameObject.Find("GameManager");
        GameManager gameManager = game.GetComponent<GameManager>();
        int initialScore = gameManager.correctAnswers;

        GameObject player = GameObject.Find("Player");
        GameObject quizBlock = GameObject.Find("QuestionBlock1");

        yield return new WaitForSeconds(1.0f);

        player.transform.position = quizBlock.transform.position;

        yield return new WaitForSeconds(2.0f);

        GameObject question = GameObject.Find("Question");
        GameObject trueButton = GameObject.Find("True");

        Assert.IsTrue(question.activeSelf, "question not activated");
        Assert.IsTrue(trueButton.activeSelf, "button not activated");

        while (question.activeSelf)
        {
            trueButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.5f);

        Assert.Greater(gameManager.correctAnswers, initialScore);
    }

    [UnityTest]
    public IEnumerator _8CheckGoToNextLevel()
    {
        GameObject player = GameObject.Find("Player");
        GameObject finish = GameObject.Find("Finish");

        yield return new WaitForSeconds(1.0f);

        player.transform.position = finish.transform.position;

        yield return new WaitForSeconds(2.0f);

        Assert.AreEqual("Level2", SceneManager.GetActiveScene().name, "Scene is: Level2");
    }

    [UnityTest]
    public IEnumerator _9CheckMenuButton()
    {
        GameObject button = GameObject.Find("ReturnToMenu");

        button.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual("MenuScreen", SceneManager.GetActiveScene().name, "Menu Scene Did Not Load");
    }

    [UnityTest]
    public IEnumerator _ACheckPlayButton()
    {
        SceneManager.LoadScene("MenuScreen");
        yield return new WaitForSeconds(1.0f);

        GameObject play = GameObject.Find("PlayButton");

        play.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        GameObject level = GameObject.Find("LevelSelect");

        Assert.IsTrue(level.activeSelf, "level select is not active");
    }

    [UnityTest]
    public IEnumerator _BCheckTutorialButton()
    {
        SceneManager.LoadScene("MenuScreen");
        yield return new WaitForSeconds(1.0f);

        GameObject tutorialButton = GameObject.Find("TutorialButton");

        tutorialButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        GameObject tutorial = GameObject.Find("Tutorial");

        Assert.IsTrue(tutorial.activeSelf, "tutorial is not active");
    }

    [UnityTest]
    public IEnumerator _CCheckReturnToMain()
    {
        SceneManager.LoadScene("MenuScreen");
        yield return new WaitForSeconds(1.0f);

        GameObject play = GameObject.Find("PlayButton");

        play.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        GameObject returnMain = GameObject.Find("ReturnToMenu");

        returnMain.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        GameObject mainMenu = GameObject.Find("Main Menu");

        Assert.IsTrue(mainMenu.activeSelf, "main menu is not active");
    }

    [UnityTest]
    public IEnumerator _DCheckLevelSelect()
    {
        SceneManager.LoadScene("MenuScreen");
        yield return new WaitForSeconds(1.0f);

        GameObject play = GameObject.Find("PlayButton");

        play.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        GameObject level1 = GameObject.Find("Level1");

        level1.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual("Level1", SceneManager.GetActiveScene().name, "Scene Did Not Load");
    }
}
