using Microsoft.AspNetCore.Mvc;
using Supplier.Core.Services;

namespace BlazorSupplier.UI;

[Route("api/[controller]")]
[ApiController]
public class ExportController : ControllerBase
{
    private readonly IExportService _exportService;

    public ExportController(IExportService exportService)
    {
        _exportService = exportService;
    }

    [HttpGet("excel")]
    public async Task<IResult> ExportToExcel(CancellationToken cancellationToken)
    {
        var fileBytes = await _exportService.ExportToExcel(cancellationToken);
        return Results.File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "products.xls");
    }

    [HttpGet("pdf")]
    public async Task<IResult> ExportToPdf(CancellationToken cancellationToken)
    {
        var fileBytes = await _exportService.ExportToPdf(cancellationToken);
        return Results.File(fileBytes, "application/pdf", "products.pdf");
    }
}
