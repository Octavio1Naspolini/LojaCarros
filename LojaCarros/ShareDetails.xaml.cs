using Dominio.Entidades;
using Integracao;
using Newtonsoft.Json;

namespace LojaCarros;

public partial class ShareDetails : ContentPage
{
    private readonly BaseClient _client = new BaseClient();
    private string _simboloAcao;
    public ShareDetails(string simboloAcao)
    {
        InitializeComponent();
        _simboloAcao = simboloAcao;
        CarregarDados(_simboloAcao);
    }

    private async Task CarregarDados(string simboloAcao)
    {
        try
        {
            HttpResponseMessage respostaAPI = await _client.GetShare(_simboloAcao);
            string conteudo = await respostaAPI.Content.ReadAsStringAsync();
            Acao acao = JsonConvert.DeserializeObject<Acao>(conteudo);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Marca.Text = $"{acao.ShortName}";
                Modelo.Text = $"{acao.ShortName}";
                Cor.Text = $"{acao.ShortName}";
                Preco.Text = $"{acao.RegularMarketPrice}";
            });
        }
        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Alerta", "Voce clicou no botao", "OK");
            });
        }
    }
}