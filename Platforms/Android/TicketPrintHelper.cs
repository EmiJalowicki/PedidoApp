using Android.OS;
using Android.Print;

namespace PedidoApp;

public class TicketPrintAdapter : PrintDocumentAdapter
{
    private readonly Android.App.Activity activity;
    private readonly TicketView ticket;

    public TicketPrintAdapter(
        Android.App.Activity activity,
        TicketView ticket)
    {
        this.activity = activity;
        this.ticket = ticket;
    }

    public override void OnLayout(
        PrintAttributes? oldAttributes,
        PrintAttributes newAttributes,
        CancellationSignal? cancellationSignal,
        LayoutResultCallback? callback,
        Bundle? extras)
    {
        if (cancellationSignal?.IsCanceled == true)
        {
            callback?.OnLayoutCancelled();
            return;
        }

        var info = new PrintDocumentInfo.Builder(
                "PedidoApp-Ticket.pdf")
            .SetContentType(PrintContentType.Document)
            .SetPageCount(1)
            .Build();

        callback?.OnLayoutFinished(info, true);
    }

    public override void OnWrite(
        PageRange[]? pages,
        ParcelFileDescriptor? destination,
        CancellationSignal? cancellationSignal,
        WriteResultCallback? callback)
    {
        if (destination == null)
        {
            callback?.OnWriteFailed(
                "No se pudo obtener el destino de impresión.");
            return;
        }

        if (cancellationSignal?.IsCanceled == true)
        {
            callback?.OnWriteCancelled();
            return;
        }

        try
        {
            var platformView =
                ticket.Handler?.PlatformView as Android.Views.View;

            if (platformView == null)
            {
                callback?.OnWriteFailed(
                    "No se pudo obtener la vista Android del ticket.");
                return;
            }

            int width = platformView.Width;
            int height = platformView.Height;

            if (width <= 0 || height <= 0)
            {
                callback?.OnWriteFailed(
                    "El ticket no tiene un tamaño válido para imprimir.");
                return;
            }

            using var bitmap = Android.Graphics.Bitmap.CreateBitmap(
                width,
                height,
                Android.Graphics.Bitmap.Config.Argb8888!);

            using (var canvas = new Android.Graphics.Canvas(bitmap))
            {
                canvas.DrawColor(Android.Graphics.Color.White);

                platformView.Draw(canvas);
            }

            using var document = new Android.Graphics.Pdf.PdfDocument();

            const int pageWidth = 595;
            const int pageHeight = 842;

            var pageInfo =
                new Android.Graphics.Pdf.PdfDocument.PageInfo.Builder(
                    pageWidth,
                    pageHeight,
                    1)
                .Create();

            using var page = document.StartPage(pageInfo);

            var pdfCanvas = page.Canvas;

            const float margin = 10f;

            float availableWidth = pageWidth - (margin * 2);
            float availableHeight = pageHeight - (margin * 2);

            float scaleX = availableWidth / width;
            float scaleY = availableHeight / height;

            float scale = Math.Min(scaleX, scaleY);

            float drawWidth = width * scale;
            float drawHeight = height * scale;

            float left = (pageWidth - drawWidth) / 2f;
            float top = (pageHeight - drawHeight) / 2f;

            var destinationRect = new Android.Graphics.RectF(
                left,
                top,
                left + drawWidth,
                top + drawHeight);

            pdfCanvas.DrawBitmap(
                bitmap,
                null,
                destinationRect,
                null);

            document.FinishPage(page);

            using var safeHandle =
                new Microsoft.Win32.SafeHandles.SafeFileHandle(
                    destination!.Fd,
                    ownsHandle: false);

            using var output = new System.IO.FileStream(
                safeHandle,
                System.IO.FileAccess.Write);

            document.WriteTo(output);

            output.Flush();

            document.Close();

            callback?.OnWriteFinished(
                new PageRange[]
                {
                PageRange.AllPages
                        });
                }
                catch (Exception ex)
                {
                    callback?.OnWriteFailed(
                        $"Error generando el ticket: {ex.Message}");
                }
    }
}