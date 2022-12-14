using UnityEngine;

public class Inventory : MonoBehaviour
{   
    private int _numberOfPotions;
    private int _gemScore;

    public int NumberOfPotions => _numberOfPotions;
    public int Score => _gemScore;

    private void Awake()
    {
        _numberOfPotions = 0;
        _gemScore = 0;
    }

    public void ChangeNumberOfPotions(int deltaNumberOfPotions)
    {
        _numberOfPotions += deltaNumberOfPotions;
    }

    public void ChangeNumberOfGems(int count)
    {
        _gemScore += count;
    }
}
