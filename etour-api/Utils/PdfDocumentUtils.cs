
using etour_api.Dtos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace etour_api.Utils;

public static class PdfDocumentUtils
{
    public static byte[] GenerateBookingPdf(TourBookingDto tourBookingDto)
    {
        using (var memoryStream = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            document.Add(new Paragraph("Booking Confirmation\n\n"));
            document.Add(new Paragraph($"Tour Name: {tourBookingDto.TourName}"));
            document.Add(new Paragraph($"Booking Date: {tourBookingDto.BookingDate}"));
            document.Add(new Paragraph($"Booking Status: {tourBookingDto.BookingStatus}"));
            document.Add(new Paragraph($"Total Price: {tourBookingDto.TotalPrice}"));
            document.Add(new Paragraph($"Category: {tourBookingDto.CategoryName}"));
            document.Add(new Paragraph($"Subcategory: {tourBookingDto.SubCategoryName}"));
            document.Add(new Paragraph($"Tour Duration: {tourBookingDto.Duration}"));
            document.Add(new Paragraph($"Start Date: {tourBookingDto.StartDate}"));
            document.Add(new Paragraph($"End Date: {tourBookingDto.EndDate}\n\n"));

            document.Add(new Paragraph("Passenger List:\n\n"));

            PdfPTable table = new PdfPTable(6)
            {
                WidthPercentage = 100
            };

            table.AddCell("Name");
            table.AddCell("Date Of Birth");
            table.AddCell("Age");
            table.AddCell("Gender");
            table.AddCell("Passenger Type");
            table.AddCell("Passenger Cost");

            foreach (var passenger in tourBookingDto.Passengers)
            {
                table.AddCell($"{passenger.FirstName} {passenger.LastName}");
                table.AddCell(passenger.DateOfBirth);
                table.AddCell(passenger.Age.ToString());
                table.AddCell(passenger.Gender.ToString());
                table.AddCell(passenger.PassengerType.ToString());
                table.AddCell(passenger.PassengerCost.ToString());
            }

            document.Add(table);
            document.Close();

            return memoryStream.ToArray();
        }
    }
}