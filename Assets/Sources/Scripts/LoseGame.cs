using UnityEngine;

public class LoseGame : MonoBehaviour
{
    [SerializeField] private SoundsPlayer _sounds;
    [SerializeField] private FinishAnimation _animation;

    public void Lose(string text)
    {
        _animation.Play(text, Color.black, Color.white);
        _sounds.Play();
    }
}