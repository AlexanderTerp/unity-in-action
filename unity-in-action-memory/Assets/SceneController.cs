using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    private const int STANDARD_UNIQUE_CARDS = 4;
    private const float INTER_CARD_BUFFER = 0.5f;

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
        for (int colIndex = 0; colIndex < _images.Length; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < 2; rowIndex++)
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

                PlaceCard(newCard, startPos, rowIndex, colIndex);
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

    private void PlaceCard(MemoryCard newCard, Vector3 startPos, int rowIndex, int colIndex)
    {
        float standardToCurrentSizingRatio = STANDARD_UNIQUE_CARDS / (float)_images.Length;

        float offsetX = newCard.transform.localScale.x + INTER_CARD_BUFFER * standardToCurrentSizingRatio;
        float offsetY = newCard.transform.localScale.y + INTER_CARD_BUFFER * standardToCurrentSizingRatio;

        float posX = (offsetX * colIndex) + startPos.x;
        float posY = -(offsetY * rowIndex) + startPos.y;
        newCard.transform.position = new Vector3(posX, posY, startPos.z);
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