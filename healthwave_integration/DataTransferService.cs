using System.Threading.Tasks;
using MyProject.CoreAPI;

public class DataTransferService
{
    private readonly ICoreAPIClient _apiAClient;
    private readonly ICoreAPIClient _apiBClient;

    public DataTransferService(ICoreAPIClient apiAClient, ICoreAPIClient apiBClient)
    {
        _apiAClient = apiAClient;
        _apiBClient = apiBClient;
    }

    public async Task TransferDataAsync()
    {
        // Obtener datos de API A
        var dataFromApiA = await _apiAClient.GetDataAsync();

        // Transformar datos si es necesario
        var transformedData = TransformData(dataFromApiA);

        // Enviar datos a API B
        await _apiBClient.PostDataAsync(transformedData);
    }

    private ApiBData TransformData(ApiAData data)
    {
        // Realizar transformación de datos si es necesario
        return new ApiBData
        {
            // Mapear propiedades
            Propiedad1 = data.Propiedad1,
            Propiedad2 = data.Propiedad2,
            // ...
        };
    }
}
