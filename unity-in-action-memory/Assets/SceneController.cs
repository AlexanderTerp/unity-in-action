using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public const int NUM_GRID_ROWS = 2;
    public const int NUM_GRID_COLS = 4;
    public const float OFFSET_X = 2.5f;
    public const float OFFSET_Y = 3.5f;

    public bool CanReveal { get { return _secondRevealed == null; } }

    [SerializeField] private MemoryCard _originalCard;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Sprite[] _images;

    private int _score = 0;
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    void Start()
    {
        List<Sprite> spritesToAssign = GenerateSpritesToAssign();

        Vector3 startPos = _originalCard.transform.position;
        for (int colIndex = 0; colIndex < NUM_GRID_COLS; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < NUM_GRID_ROWS; rowIndex++)
            {
                MemoryCard newCard;
                if (colIndex == 0 && rowIndex == 0)
                {
                    newCard = _originalCard;
                }
                else
                {
                    newCard = Instantiate(_originalCard) as MemoryCard;
                }

                int id = Random.Range(0, spritesToAssign.Count);
                Sprite spriteToAssign = spritesToAssign[id];
                spritesToAssign.RemoveAt(id);
                newCard.SetCard(spriteToAssign.GetHashCode(), spriteToAssign);

                float posX = (OFFSET_X * colIndex) + startPos.x;
                float posY = -(OFFSET_Y * rowIndex) + startPos.y;
                newCard.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score++;
            scoreText.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    private List<Sprite> GenerateSpritesToAssign()
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (Sprite sprite in _images)
        {
            for (int i = 0; i < 2; i++)
            {
                sprites.Add(sprite);
            }
        }
        return sprites;
    }
}