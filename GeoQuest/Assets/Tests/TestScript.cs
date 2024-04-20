using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public class TestScript
{
    [UnityTest]
    public IEnumerator CheckPlayerSpawn()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckPlayerMovement()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckPlayerJump()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckKillzone()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckMenuButton()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckQuizActivation()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckWallDeactivation()
    {
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator CheckGoToNextLevel()
    {
        yield return new WaitForSeconds(.1f);
    }
}
