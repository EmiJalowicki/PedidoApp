using Android.OS;
using Android.Print;
using Android.Util;
using Android.Views;
using static Android.Views.View;

namespace PedidoApp;

public class TicketPrintAdapter : PrintDocumentAdapter
{
    private readonly Android.App.Activity activity;
    private readonly TicketView ticket;

    private PrintAttributes? printAttributes;

    // =============================================================
    // ANCHO LÓGICO DEL TICKET
    // =============================================================
    //
    // MAUI/Android utiliza dp para el layout.
    //
    // 360 dp es aproximadamente el ancho lógico que tenemos
    // funcionando correctamente en el teléfono.
    //
    private const float RenderWidthDp = 360f;

    public TicketPrintAdapter(
        Android.App.Activity activity,
        TicketView ticket)
    {
        this.activity = activity;
        this.ticket = ticket;
    }

    public override void OnLayout(
        PrintAttributes? oldAttributes,
        PrintAttributes? newAttributes,
        CancellationSignal? cancellationSignal,
        LayoutResultCallback? callback,
        Bundle? extras)
    {
        if (cancellationSignal?.IsCanceled == true)
        {
            callback?.OnLayoutCancelled();
            return;
        }

        // Guardamos la configuración de impresión elegida
        // por Android Print.
        printAttributes = newAttributes;

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
            // =====================================================
            // OBTENER LA VISTA ANDROID DEL TICKET
            // =====================================================

            var platformView =
                ticket.Handler?.PlatformView as Android.Views.View;

            if (platformView == null)
            {
                callback?.OnWriteFailed(
                    "No se pudo obtener la vista Android del ticket.");
                return;
            }

            // =====================================================
            // OBTENER DENSIDAD DEL DISPOSITIVO
            // =====================================================

            float density =
                activity.Resources?.DisplayMetrics?.Density ?? 1f;

            if (density <= 0)
                density = 1f;

            // Convertimos nuestro ancho lógico (dp)
            // a píxeles físicos.
            //
            // Ejemplo:
            //
            // Teléfono:
            // 360 dp × 3.0 = 1080 px
            //
            // Tablet:
            // 360 dp × 1.5 = 540 px
            //

            int renderWidthPx =
                (int)Math.Round(
                    RenderWidthDp * density);

            if (renderWidthPx <= 0)
            {
                callback?.OnWriteFailed(
                    "No se pudo calcular el ancho de renderizado.");
                return;
            }

            // =====================================================
            // GUARDAR TAMAÑO ORIGINAL
            // =====================================================

            int originalWidth = platformView.Width;
            int originalHeight = platformView.Height;

            // =====================================================
            // MEDIR EL TICKET
            // =====================================================

            platformView.Measure(
                MeasureSpec.MakeMeasureSpec(
                    renderWidthPx,
                    MeasureSpecMode.Exactly),

                MeasureSpec.MakeMeasureSpec(
                    0,
                    MeasureSpecMode.Unspecified));

            int renderHeightPx =
                platformView.MeasuredHeight;

            if (renderHeightPx <= 0)
            {
                callback?.OnWriteFailed(
                    "El ticket no tiene una altura válida para imprimir.");
                return;
            }

            // =====================================================
            // PREPARAR EL LAYOUT
            // =====================================================

            platformView.Layout(
                0,
                0,
                renderWidthPx,
                renderHeightPx);

            // =====================================================
            // CREAR BITMAP
            // =====================================================

            using var bitmap =
                Android.Graphics.Bitmap.CreateBitmap(
                    renderWidthPx,
                    renderHeightPx,
                    Android.Graphics.Bitmap.Config.Argb8888!);

            using (var canvas =
                   new Android.Graphics.Canvas(bitmap))
            {
                canvas.DrawColor(
                    Android.Graphics.Color.White);

                platformView.Draw(canvas);
            }

            // =====================================================
            // RESTAURAR EL TAMAÑO ORIGINAL
            // =====================================================

            if (originalWidth > 0 && originalHeight > 0)
            {
                platformView.Layout(
                    platformView.Left,
                    platformView.Top,
                    platformView.Left + originalWidth,
                    platformView.Top + originalHeight);
            }

            // =====================================================
            // OBTENER TAMAÑO DE PAPEL
            // =====================================================

            var mediaSize =
                printAttributes?.GetMediaSize();

            if (mediaSize == null)
            {
                callback?.OnWriteFailed(
                    "No se pudo determinar el tamaño del papel.");
                return;
            }

            // WidthMils está expresado en milésimas de pulgada.
            //
            // 1000 mils = 1 pulgada
            // 1 pulgada = 72 puntos PDF

            float pageWidth =
                mediaSize.WidthMils / 1000f * 72f;

            if (pageWidth <= 0)
            {
                callback?.OnWriteFailed(
                    "El ancho del papel no es válido.");
                return;
            }

            // =====================================================
            // MÁRGENES
            // =====================================================

            const float margin = 5f;

            float availableWidth =
                pageWidth - margin * 2;

            if (availableWidth <= 0)
            {
                callback?.OnWriteFailed(
                    "El ancho disponible de impresión no es válido.");
                return;
            }

            // =====================================================
            // ESCALAR AL ANCHO DEL PAPEL
            // =====================================================

            //
            // El TicketView ya fue renderizado con el mismo ancho
            // lógico en todos los dispositivos.
            //
            // Ahora simplemente lo llevamos al ancho físico
            // del papel.
            //

            float scale =
                availableWidth / renderWidthPx;

            float drawWidth =
                renderWidthPx * scale;

            float drawHeight =
                renderHeightPx * scale;

            // =====================================================
            // ALTURA DEL PDF
            // =====================================================

            float pageHeight =
                drawHeight + margin * 2;

            if (pageHeight <= 0)
            {
                callback?.OnWriteFailed(
                    "La altura de la página no es válida.");
                return;
            }

            // =====================================================
            // CREAR PDF
            // =====================================================

            using var document =
                new Android.Graphics.Pdf.PdfDocument();

            var pageInfo =
                new Android.Graphics.Pdf.PdfDocument.PageInfo.Builder(
                    (int)Math.Ceiling(pageWidth),
                    (int)Math.Ceiling(pageHeight),
                    1)
                .Create();

            using var page =
                document.StartPage(pageInfo);

            if (page == null)
            {
                callback?.OnWriteFailed(
                    "No se pudo crear la página de PDF.");
                return;
            }

            var pdfCanvas = page.Canvas;

            if (pdfCanvas == null)
            {
                callback?.OnWriteFailed(
                    "No se pudo obtener el canvas del PDF.");
                return;
            }

            // =====================================================
            // DIBUJAR TICKET
            // =====================================================

            float left =
                (pageWidth - drawWidth) / 2f;

            float top = margin;

            var destinationRect =
                new Android.Graphics.RectF(
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

            // =====================================================
            // ESCRIBIR PDF
            // =====================================================

            using var safeHandle =
                new Microsoft.Win32.SafeHandles.SafeFileHandle(
                    destination.Fd,
                    ownsHandle: false);

            using var output =
                new System.IO.FileStream(
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