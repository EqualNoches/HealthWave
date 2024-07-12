using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DataTransferController : ControllerBase
{
    private readonly DataTransferService _dataTransferService;

    public DataTransferController(DataTransferService dataTransferService)
    {
        _dataTransferService = dataTransferService;
    }

    [HttpPost("transfer-data")]
    public async Task<IActionResult> TransferData()
    {
        await _dataTransferService.TransferDataAsync();
        return Ok();
    }
}
