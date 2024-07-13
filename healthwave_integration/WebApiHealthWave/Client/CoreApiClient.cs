namespace WebApiHealthWave.Client
{
    public class CoreApiClient
    {
        _dbContext =dbContext;
    }

public async Task <int>
CreateAfeccionAsync(WebApiHealthWave.models.Afeccion afeccion)
{
    try{
        int IdAfeccion =
        checked((int) afeccion.Id);
        await
        WebApiHealthWave.ApiAfeccionAddAsync(IdAfeccion, afeccion.Nombre, afeccion.Descripcion, [],[]);
        return1;

    }
catch (Exception)
        {
            _dbContext.Afeccion,Add(afeccion);
            await _dbContext.SaveChangesAsync();
            return1;
        }

    }
public async
Task<List<WebApiHealthWave.models.Afeccion>>
GetAfeccionAsync()
{
    try{
        var afeccionesFromApi = await
        WebApiHealthWave.ApiAfeccionGetGetAsync();
        var afeccionesList = new List<WebApiHealthWave.models.Afeccion>();
        foreach (var afeccion in afeccion) { afeccionesList.Add(new WebApiHealthWave.models.Afeccion())}

        {
            IDAfeccion = (uint) afeccionesFromApi.Id,
            Nombre afeccionFromApi.Nombre, 
            Descripcion = afeccionFromApi.Descripcion,
        };
        return afeccionesList;

        }
        catch (Exception)
        {
            return await
            _dbContext.Afeccions.ToListAsync();
        }

    }
    public async
    Task<WebApiHealthWave.Models.Afeccion> GetAfeccionByIdAsync(int id)

    {
        try{
            var afeccionesFromApi = await
            WebApiHealthWave.ApiAfecionesGetGetAsync(id);

            var modelAfeccion = new WebApiHealthWave.models.Afeccion
            {
                IDAfeccion = (uint)coreAfeccion.IDAfeccion,
                Nombre = coreAfeccion.Nombre,
                Descripcion = coreAfeccion.Descripcion,
            };

return modelAfeccion;

        }

        catch (ApiException ex)
        {
            return await
            _dbContext.Afeccions.FindAsync(id);
        
        }
    }
    public async Task <int> UpdateAfeccionesAsync(WebApiHealthWave.Models.Afeccion afeccion)

}
{
    try
    {
int IDAfeccionint =checked((int)afeccion.IDAfeccion);

await
WebApiHealthWave.ApiAfeccionUpdateAsync(IDAfeccionint,afeccion.Nombre, afeccion.Descripcion, [],[]);
return1;

    }
    catch (Exception)
    {
        var afeccion = await
        _dbContext.Afeccions.FindAsync(id); if (afeccion != null)
        {
            _dbContext.Afeccions.Remove(afeccion);
            await _dbContext.SaveChangesAsync();
            return1;
        }
        return 0;
    }
}
//}

//}
