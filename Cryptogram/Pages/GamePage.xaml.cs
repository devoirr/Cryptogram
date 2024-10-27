using Android.Widget;
using Cryptogram.Models;
using Microsoft.Maui.Controls.Shapes;
using Button = Microsoft.Maui.Controls.Button;

namespace Cryptogram.Pages;

public partial class GamePage
{
    private readonly Dictionary<char, int> _numbers = new();
    private readonly List<LetterLayoutData> _letters = [];

    private int _currentLetter = -1;
    
    public GamePage(string quoteLower)
    {
        InitializeComponent();
        var quote = quoteLower.ToUpper();

        foreach (var letter in quote)
        {
            if (letter is '!' or '.' or ',' or '-')
                continue;
            
            if (!_numbers.ContainsKey(letter))
            {
                _numbers.Add(letter, GetRandomNumber());
            }
        }
        
        var words = quote.Split(' ');
        var lines = new List<string>();

        var currentLine = "";
        foreach (var word in words)
        {
            if (currentLine.Length + word.Length < 20)
            {
                currentLine += word + " ";
            }
            else
            {
                lines.Add(currentLine);
                currentLine = word + " ";
            }
        }
        
        lines.Add(currentLine);

        foreach (var line in lines)
        {
            var lineLayout = new HorizontalStackLayout();
            foreach (var word in line.Split(' '))
            {
                lineLayout.Children.Add(BuildStackLayout(word));
            }
            
            quoteLayout.Children.Add(lineLayout);
        }
        
    }

    private void OnKeyClicked(object? sender, EventArgs e)
    {
        if (_currentLetter == -1)
            return;
        
        var button = (Button)sender!;
        var letter = button.Text[0];
        
        foreach (var letterLayoutData in _letters.Where(data => data.Number == _currentLetter))
        {
            (letterLayoutData.Layout.Children[0] as Label).Text = letter.ToString();
            letterLayoutData.SetChosenLetter(letter.ToString());
        }
        
        foreach (var data in _letters)
        {
            if (data.ChosenLetter == null || data.ChosenLetter != data.Letter.ToString())
                return;
        }
        
        quoteLayout.Children.Clear();
        quoteLayout.Children.Add(new Label{ Text = "You Won!", HorizontalTextAlignment = TextAlignment.Center});
        
    }

    private HorizontalStackLayout BuildStackLayout(string word)
    {
        var layout = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10,0,10,0)
        };

        foreach (var letter in word)
        {
            if (letter is '.' or '!' or ',' or '-')
            {
                layout.Children.Add(new Label { Text = letter.ToString(), FontSize = 26, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center });
            }
            else
            {
                layout.Children.Add(BuildLetterLayout(letter));
            }
        }
        
        return layout;
    }

    private VerticalStackLayout BuildLetterLayout(char letter)
    {
        var layout = new VerticalStackLayout { HorizontalOptions = LayoutOptions.Center, Padding = 0};
        
        layout.Children.Add(new Label { Text = "", FontSize = 26, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Colors.Black});
        layout.Children.Add(new Rectangle { BackgroundColor = Colors.Black});
        layout.Children.Add(new Label { Text = _numbers[letter].ToString(), FontSize = 14, HorizontalTextAlignment = TextAlignment.Center});

        layout.WidthRequest = 20;
        layout.HeightRequest = 60;
        
        _letters.Add(new LetterLayoutData(layout, letter, _numbers[letter], null));
        
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += LetterTapped;
        layout.GestureRecognizers.Add(tapGestureRecognizer);
        
        return layout;
    }

    private void LetterTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not VerticalStackLayout layout)
        {
            Console.WriteLine("1");
            return;
        }

        if (layout.Children.Count < 3)
        {
            Console.WriteLine("2");
            return;
        }
        
        var child = layout.Children[2];

        if (child is not Label l)
        {
            Console.WriteLine("3");
            return;
        }
        
        var number = int.Parse(l.Text);
        _currentLetter = number;
        foreach (var letterLayoutData in _letters)
        {
            letterLayoutData.Layout.BackgroundColor = letterLayoutData.Number == _currentLetter ? Colors.LightSteelBlue : Colors.Transparent;
        }
    }

    private int GetRandomNumber()
    {
        var random = new Random();
        var number = random.Next(1, 27);

        while (_numbers.ContainsValue(number))
        {
            number = random.Next(1, 27);
        }

        return number;
    }
    
}