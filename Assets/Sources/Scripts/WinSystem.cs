using System.Collections;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    [SerializeField] private SoundsPlayer _sounds;
    [SerializeField] private FinishAnimation _animation;

    private Color Black => Color.black;

    public void Win(string text)
    {
        _animation.Play(text, new Color(Black.r, Black.g, Black.g, 0.8f), Color.white);
        _sounds.Play();
        StartCoroutine(StartHappyWin());
    }

    private IEnumerator StartHappyWin()
    {
        yield return new WaitForSeconds(3);
        _animation.Stop("Thanks for playing my game.");
    }
}
