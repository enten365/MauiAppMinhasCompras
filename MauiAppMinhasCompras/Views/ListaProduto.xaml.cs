namespace MauiAppMinhasCompras.Views;

using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        lista.Clear();
        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var produto = menuItem?.CommandParameter as Produto;

        if (produto == null)
            return;

        bool confirmar = await DisplayAlert("Confirmação",
            $"Deseja realmente excluir o produto {produto.Descricao}?",
            "Sim", "Não");

        if (confirmar)
        {
            await App.Db.Delete(produto.Id);

            lista.Remove(produto);

            await DisplayAlert("Sucesso", "Produto excluído!", "OK");
        }
    }
}