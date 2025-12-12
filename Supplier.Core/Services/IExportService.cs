namespace Supplier.Core.Services;

public interface IExportService
{
    Task<byte[]> ExportToExcel(CancellationToken cancellationToken);
    Task<byte[]> ExportToPdf(CancellationToken cancellationToken);
}