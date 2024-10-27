namespace Cryptogram.Models;

public class LetterLayoutData
{
    private readonly VerticalStackLayout _layout;
    private char letter;
    private int number;
    private string? chosenLetter;
    
    public LetterLayoutData(VerticalStackLayout layout, char letter, int number, string? chosenLetter)
    {
        _layout = layout;
        this.letter = letter;
        this.number = number;
        this.chosenLetter = chosenLetter;
    }
    
    public VerticalStackLayout Layout => _layout;
    public char Letter => letter;
    
    public int Number => number;
    
    public string? ChosenLetter => chosenLetter;

    public void SetChosenLetter(string? chosenLetter)
    {
        this.chosenLetter = chosenLetter;
    }
}