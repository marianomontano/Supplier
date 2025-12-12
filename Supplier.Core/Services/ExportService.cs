using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Supplier.Core.Services;

public class ExportService : IExportService
{

    private readonly ICacheService _cacheService;
    private readonly ILogger<ExportService> _logger;

    public ExportService(ICacheService cacheService, ILogger<ExportService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<byte[]> ExportToExcel(CancellationToken cancellationToken)
    {
        try
        {
            var products = await _cacheService.GetOrSetProducts();
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Products");

            // Headers
            worksheet.Cell(1, 1).Value = "Código";
            worksheet.Cell(1, 2).Value = "Nombre";
            worksheet.Cell(1, 3).Value = "Costo";
            worksheet.Cell(1, 4).Value = "Público";
            worksheet.Cell(1, 5).Value = "Categoría";
            worksheet.Cell(1, 6).Value = "Stock";
            worksheet.Cell(1, 7).Value = "Activo";

            // Body
            var row = 2;
            foreach (var prod in products)
            {

                worksheet.Cell(row, 1).Value = prod.Code;
                worksheet.Cell(row, 2).Value = prod.Name;
                worksheet.Cell(row, 3).Value = prod.Cost;
                worksheet.Cell(row, 4).Value = prod.Public;
                worksheet.Cell(row, 5).Value = prod.Provider;
                worksheet.Cell(row, 6).Value = prod.Stock;
                worksheet.Cell(row, 7).Value = prod.IsActive ? "Sí" : "No";
                row++;
            }

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;

            return ms.ToArray();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<byte[]> ExportToPdf(CancellationToken cancellationToken)
    {
        try
        {
            var products = await _cacheService.GetOrSetProducts(cancellationToken);

            QuestPDF.Settings.License = LicenseType.Community;
            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(QuestPDF.Helpers.PageSizes.A4);
                    page.Margin(20);
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        // Header
                        table.Header(header =>
                        {
                            header.Cell().Text("Código");
                            header.Cell().Text("Nombre");
                            header.Cell().Text("Costo");
                            header.Cell().Text("Público");
                        });

                        // Rows
                        foreach (var prod in products)
                        {
                            table.Cell().Text(prod.Code);
                            table.Cell().Text(prod.Name);
                            table.Cell().Text(prod.Cost.ToString("C"));
                            table.Cell().Text(prod.Public.ToString("C"));
                        }
                    });
                });
            });


            byte[] pdfBytes = pdf.GeneratePdf();
            return pdfBytes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
