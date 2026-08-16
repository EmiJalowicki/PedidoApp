using Android.Content;
using Android.Print;
using Microsoft.Maui.ApplicationModel;

namespace PedidoApp;

public static class PrintHelper
{
    public static void Print(TicketView ticket)
    {
        var activity = Platform.CurrentActivity;

        if (activity == null)
            return;

        var printManager =
            (PrintManager)activity.GetSystemService(Context.PrintService)!;

        var adapter = new TicketPrintAdapter(activity, ticket);

        printManager.Print(
            "PedidoApp - Ticket",
            adapter,
            null);
    }
}