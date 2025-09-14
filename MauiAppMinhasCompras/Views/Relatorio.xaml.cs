using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
namespace MauiAppMinhasCompras.Views;

public partial class Relatorio : ContentPage
{
    ObservableCollection<Produto> lista_relatorio = new ObservableCollection<Produto>();

    public Relatorio()
	{
        InitializeComponent();
        lst_relatorio.ItemsSource = lista_relatorio;
    }

    private async void Filtrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var inicio = dp_inicio.Date;
            var fim = dp_fim.Date.AddDays(1).AddTicks(-1); 

            var todos = await App.Db.GetAll();
            var filtrados = todos
                .Where(p => p.DataCadastro >= inicio && p.DataCadastro <= fim)
                .OrderBy(p => p.DataCadastro)
                .ToList();

            lista_relatorio.Clear();
            foreach (var p in filtrados)
                lista_relatorio.Add(p);

            if (!filtrados.Any())
                await DisplayAlert("Aviso", "Nenhum produto encontrado no período.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecinado = sender as MenuItem;

            Produto p = selecinado.BindingContext as Produto;

            bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista_relatorio.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
