namespace Cryptogram.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("CLICK");
    }

    private void Test_OnClicked(object? sender, EventArgs e)
    {
        App.Current.MainPage = new GamePage("this is a test quote. this is the best quote.");
    }
}