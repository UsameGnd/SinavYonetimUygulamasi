using System;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SinavYonetimUygulamasi.Models;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SinavYonetimUygulamasi.Services
{
    public interface IPdfService
    {
        Task<byte[]> GeneratePdfFromView<T>(string viewName, T model);
    }

    public class PdfService : IPdfService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<PdfService> _logger;

        public PdfService(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IWebHostEnvironment hostEnvironment,
            ILogger<PdfService> logger)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public async Task<byte[]> GeneratePdfFromView<T>(string viewName, T model)
        {
            try
            {
                _logger.LogInformation($"Starting PDF generation for view: {viewName}");
                
                // Create a memory stream for the PDF output
                using var pdfStream = new MemoryStream();
                
                // Create the PDF writer and document
                using var writer = new PdfWriter(pdfStream);
                using var pdf = new PdfDocument(writer);
                using var document = new Document(pdf);
                
                // Set up fonts
                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                
                // Add title
                var title = new Paragraph(model.GetType().GetProperty("Title")?.GetValue(model)?.ToString() ?? "Sınav")
                    .SetFont(boldFont)
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(title);

                // Add course info
                var courseName = model.GetType().GetProperty("Course")?.GetValue(model)?.GetType().GetProperty("Name")?.GetValue(model.GetType().GetProperty("Course")?.GetValue(model))?.ToString();
                if (!string.IsNullOrEmpty(courseName))
                {
                    document.Add(new Paragraph($"Ders: {courseName}")
                        .SetFont(font)
                        .SetFontSize(12)
                        .SetMarginBottom(10));
                }

                // Add questions
                var examQuestions = model.GetType().GetProperty("ExamQuestions")?.GetValue(model) as IEnumerable<dynamic>;
                if (examQuestions != null)
                {
                    int questionNumber = 1;
                    foreach (var examQuestion in examQuestions.OrderBy(eq => eq.QuestionOrder))
                    {
                        var question = examQuestion.Question;
                        if (question != null)
                        {
                            // Add question text
                            document.Add(new Paragraph($"{questionNumber}. {question.QuestionText}")
                                .SetFont(boldFont)
                                .SetFontSize(12)
                                .SetMarginBottom(10));

                            // Add options
                            var options = question.Options as IEnumerable<dynamic>;
                            if (options != null)
                            {
                                foreach (var option in options.OrderBy(o => o.OrderIndex))
                                {
                                    document.Add(new Paragraph($"    {char.ConvertFromUtf32(65 + option.OrderIndex)}. {option.OptionText}")
                                        .SetFont(font)
                                        .SetFontSize(11)
                                        .SetMarginBottom(5));
                                }
                            }

                            document.Add(new Paragraph("").SetMarginBottom(20));
                            questionNumber++;
                        }
                    }
                }

                document.Close();
                
                var pdfBytes = pdfStream.ToArray();
                _logger.LogInformation($"PDF generated successfully. Size: {pdfBytes.Length} bytes");
                return pdfBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF");
                throw;
            }
        }
    }
} 