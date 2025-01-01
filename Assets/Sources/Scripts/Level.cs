using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemyRoot _enemyRoot;
    [SerializeField] private Kitty _kitty;
    [SerializeField] private Timer _timer;
    [SerializeField] private WinSystem _winSystem;
    [SerializeField] private LoseGame _loseGame;
    [SerializeField] private Image _image;

    private IEnumerator Start()
    {
        _image.color = Color.black;

        _player.Init();
        yield return null;
        _enemyRoot.Init(_player);
        yield return null;
        _kitty.Init();
        yield return null;
        _timer.Init();
        yield return null;
        _image.DOFade(0, 2).onComplete += () => _image.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _enemyRoot.Losed += OnLosed;
        _player.Losed += OnLosed;
        _kitty.Losed += OnLosed;
        _timer.Winned += OnWinned;
    }

    private void OnDisable()
    {
        _enemyRoot.Losed -= OnLosed;
        _player.Losed -= OnLosed;
        _kitty.Losed -= OnLosed;
    }

    private void OnLosed(string text)
    {
        StopGame();
        _loseGame.Lose(text);
        StartCoroutine(RestartGame());
    }

    private void OnWinned(string text)
    {
        StopGame();
        _winSystem.Win(text);
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    [ContextMenu(nameof(StopGame))]
    private void StopGame()
    {
        _enemyRoot.Disable();
        _kitty.Disable();
        _player.Disable();
        _timer.Disable();
        StopAllCoroutines();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        SceneManager.LoadScene(0);
    //}
}
