using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : Singleton<SceneTransitioner>
{
    [SerializeField] private Animator anim;
    private bool isLoading;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ToNextScene();
    }

    public void ToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (!isLoading)
            StartCoroutine(WaitThenLoadScene(nextSceneIndex));
    }

    public void ToFirstScene()
    {
        if (!isLoading)
            StartCoroutine(WaitThenLoadScene(0));
    }

    private IEnumerator WaitThenLoadScene(int _index)
    { 
        isLoading = true;
        anim.SetTrigger("FadeToBlack");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(_index);
    }
}
