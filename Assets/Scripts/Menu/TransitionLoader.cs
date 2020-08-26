using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLoader : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject imageloading;
    public float transitionTime = 1f;

    public IEnumerator LoadLevel(int levelIndex)
    {
        transitionAnim.SetBool("StartTransition", true);

        yield return new WaitForSeconds(1);

        imageloading.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        imageloading.SetActive(false);
        transitionAnim.SetBool("StartTransition", false);
        SceneManager.LoadScene(levelIndex);
    }
}
