namespace MauiAppMinhasCompras.Views;

using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;

public partial class ListaProduto : ContentPage
{
    private readonly SQLiteDatabaseHelper _db;

    public ListaProduto()
    {
        InitializeComponent();
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "teste.db3");
        _db = new SQLiteDatabaseHelper(dbPath);
    }

    private async void OnInserir(object sender, EventArgs e)
    {
        await _db.Insert(new Produto { Descricao = "Banana", Quantidade = 5, Preco = 2.5 });
        ResultadoLabel.Text = "Produto inserido.";
    }

    private async void OnListar(object sender, EventArgs e)
    {
        var produtos = await _db.GetAll();
        ResultadoLabel.Text = string.Join("\n", produtos.Select(p => $"{p.Id} - {p.Descricao}"));
    }

    private async void OnAtualizar(object sender, EventArgs e)
    {
        var produtos = await _db.GetAll();
        if (produtos.Any())
        {
            var p = produtos.First();
            p.Descricao += " (Editado)";
            await _db.Update(p);
            ResultadoLabel.Text = "Primeiro produto atualizado.";
        }
        else ResultadoLabel.Text = "Nenhum produto para atualizar.";
    }

    private async void OnExcluir(object sender, EventArgs e)
    {
        var produtos = await _db.GetAll();
        if (produtos.Any())
        {
            await _db.Delete(produtos.First().Id);
            ResultadoLabel.Text = "Primeiro produto excluído.";
        }
        else ResultadoLabel.Text = "Nenhum produto para excluir.";
    }
}