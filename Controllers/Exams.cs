using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavYonetimUygulamasi.Context;
using SinavYonetimUygulamasi.Models;
using SinavYonetimUygulamasi.Services;
using SinavYonetimUygulamasi.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SinavYonetimUygulamasi.Controllers;

[Authorize(Roles = "Egitmen,Admin")]
public class Exams : Controller
{
    private readonly AppDbContext _context;

    public Exams(AppDbContext context)
    {
        _context = context;
    }

    // Show exam creation form
    [HttpGet]
    public IActionResult CreateExam()
    {
        ViewBag.Courses = _context.Courses.ToList();
        return View();
    }

    // Get topics for a specific course (AJAX)
    [HttpGet]
    public IActionResult GetTopics(int courseId)
    {
        var topics = _context.Topics
            .Where(t => t.CourseId == courseId)
            .Select(t => new { t.Id, t.Name })
            .ToList();
        return Json(topics);
    }

    // Get subtopics for a specific topic (AJAX)
    [HttpGet]
    public IActionResult GetSubtopics(int topicId)
    {
        var subtopics = _context.SubTopics
            .Where(s => s.TopicId == topicId)
            .Select(s => new { s.Id, s.Name })
            .ToList();
        return Json(subtopics);
    }

    // Manual question selection
    [HttpPost]
    public async Task<IActionResult> ManualQuestionSelection(int courseId, int topicId, int? subtopicId, string difficulty)
    {
        try
        {
            // Önce ders ve konunun varlığını kontrol et
            var course = await _context.Courses.FindAsync(courseId);
            var topic = await _context.Topics.FindAsync(topicId);

            if (course == null || topic == null)
            {
                ViewBag.ErrorMessage = "Seçilen ders veya konu bulunamadı.";
                var courses = await _context.Courses.ToListAsync();
                ViewBag.Courses = courses;
                return View("CreateExam");
            }

            // Alt konu seçilmişse kontrol et
            if (subtopicId.HasValue)
            {
                var subtopic = await _context.SubTopics.FindAsync(subtopicId.Value);
                if (subtopic == null)
                {
                    ViewBag.ErrorMessage = "Seçilen alt konu bulunamadı.";
                    var courses = await _context.Courses.ToListAsync();
                    ViewBag.Courses = courses;
                    return View("CreateExam");
                }
            }

            // Soruları getir
            var query = _context.QuestionBanks
                .Include(q => q.SubTopic)
                    .ThenInclude(s => s.Topic)
                        .ThenInclude(t => t.Course)
                .Include(q => q.Options)
                .Where(q => q.SubTopic.Topic.CourseId == courseId && q.SubTopic.TopicId == topicId);

            if (subtopicId.HasValue)
            {
                query = query.Where(q => q.SubTopicId == subtopicId.Value);
            }

            if (!string.IsNullOrEmpty(difficulty) && difficulty != "Herhangi")
            {
                query = query.Where(q => q.DifficultyLevel == (difficulty == "1" ? DifficultyLevel.Easy : difficulty == "2" ? DifficultyLevel.Medium : difficulty == "3" ? DifficultyLevel.Hard : DifficultyLevel.Easy));
            }

            var questions = await query.ToListAsync();

            if (!questions.Any())
            {
                ViewBag.ErrorMessage = "Belirtilen kriterlere uygun soru bulunamadı.";
                var courses = await _context.Courses.ToListAsync();
                ViewBag.Courses = courses;
                return View("CreateExam");
            }

            // Seçili soruları session'dan al
            var selectedQuestions = HttpContext.Session.GetObject<List<int>>("SelectedQuestions") ?? new List<int>();
            ViewBag.SelectedQuestions = selectedQuestions;

            return View("QuestionSelection", questions);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "Soru seçimi sırasında bir hata oluştu: " + ex.Message;
            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = courses;
            return View("CreateExam");
        }
    }

    // Add question to selection
    [HttpPost]
    public IActionResult AddQuestionToSelection(int questionId)
    {
        var selectedQuestions = HttpContext.Session.GetObject<List<int>>("SelectedQuestions") ?? new List<int>();
        
        if (!selectedQuestions.Contains(questionId))
        {
            selectedQuestions.Add(questionId);
            HttpContext.Session.SetObject("SelectedQuestions", selectedQuestions);
        }

        return Json(new { success = true });
    }

    // Remove question from selection
    [HttpPost]
    public IActionResult RemoveQuestionFromSelection(int questionId)
    {
        var selectedQuestions = HttpContext.Session.GetObject<List<int>>("SelectedQuestions") ?? new List<int>();
        selectedQuestions.Remove(questionId);
        HttpContext.Session.SetObject("SelectedQuestions", selectedQuestions);
        return Json(new { success = true });
    }

    // View selected questions
    [HttpGet]
    public IActionResult ViewSelectedQuestions()
    {
        var selectedQuestions = HttpContext.Session.GetObject<List<int>>("SelectedQuestions") ?? new List<int>();
        
        var questions = _context.QuestionBanks
            .Include(q => q.SubTopic)
            .Where(q => selectedQuestions.Contains(q.Id))
            .Select(q => new
            {
                q.Id,
                q.QuestionText,
                q.DifficultyLevel,
                SubTopic = new { q.SubTopic.Id, q.SubTopic.Name }
            })
            .ToList();

        return PartialView("_SelectedQuestions", questions);
    }

    // Clear selected questions
    [HttpPost]
    public IActionResult ClearSelectedQuestions()
    {
        try
        {
            HttpContext.Session.Remove("SelectedQuestions");
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    // Save exam with selected questions
    [HttpPost]
    public async Task<IActionResult> SaveSelectedQuestionsExam(
        string title,
        string description,
        int courseId,
        int examDuration,
        int passingScore,
        bool randomizeQuestions,
        bool showResults,
        bool allowReview,
        bool isDraft = false)
    {
        try
        {
            var selectedQuestions = HttpContext.Session.GetObject<List<int>>("SelectedQuestions") ?? new List<int>();
            
            if (!selectedQuestions.Any())
            {
                return Json(new { success = false, error = "Lütfen en az bir soru seçin." });
            }

            // Validate course exists
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return Json(new { success = false, error = "Seçilen ders bulunamadı." });
            }

            // Validate all questions exist
            var questions = await _context.QuestionBanks
                .Where(q => selectedQuestions.Contains(q.Id))
                .ToListAsync();

            if (questions.Count != selectedQuestions.Count)
            {
                return Json(new { success = false, error = "Bazı seçili sorular bulunamadı." });
            }

            // Create new exam
            var exam = new Exam
            {
                Title = title,
                Description = description,
                CourseId = courseId,
                ExamDuration = examDuration,
                PassingScore = passingScore,
                RandomizeQuestions = randomizeQuestions,
                ShowResults = showResults,
                AllowReview = allowReview,
                IsDraft = isDraft,
                Status = isDraft ? ExamStatus.Draft : ExamStatus.Published,
                CreatedBy = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1"),
                CreatedAt = DateTime.Now
            };

            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();

            // Add selected questions to exam
            var examQuestions = selectedQuestions.Select((questionId, index) => new ExamQuestion
            {
                ExamId = exam.Id,
                QuestionId = questionId,
                QuestionOrder = index + 1,
                CreatedAt = DateTime.Now
            });

            await _context.ExamQuestions.AddRangeAsync(examQuestions);
            await _context.SaveChangesAsync();

            // Clear selected questions from session
            HttpContext.Session.Remove("SelectedQuestions");

            return Json(new { success = true, examId = exam.Id });
        }
        catch (DbUpdateException dbEx)
        {
            var innerException = dbEx.InnerException;
            var errorMessage = "Veritabanı güncelleme hatası: ";
            
            if (innerException != null)
            {
                errorMessage += innerException.Message;
                if (innerException.InnerException != null)
                {
                    errorMessage += " - " + innerException.InnerException.Message;
                }
            }
            else
            {
                errorMessage += dbEx.Message;
            }

            return Json(new { success = false, error = errorMessage });
        }
        catch (Exception ex)
        {
            var errorMessage = "Sınav kaydedilirken bir hata oluştu: " + ex.Message;
            if (ex.InnerException != null)
            {
                errorMessage += " - " + ex.InnerException.Message;
            }
            return Json(new { success = false, error = errorMessage });
        }
    }

    // Generate exam questions based on criteria
    [HttpPost]
    public async Task<IActionResult> GenerateExam(int courseId, int[] topicIds, int questionCount, string difficulty, string selectionType)
    {
        try
        {
            // Build query to filter questions
            var query = _context.QuestionBanks
                .Include(q => q.SubTopic)
                    .ThenInclude(s => s.Topic)
                .Include(q => q.Options)
                .AsQueryable();

            // Filter by course
            query = query.Where(q => q.SubTopic.Topic.CourseId == courseId);

            // Filter by topics if specified
            if (topicIds != null && topicIds.Any())
            {
                query = query.Where(q => topicIds.Contains(q.SubTopic.TopicId));
            }

            // Filter by difficulty if specified
            if (!string.IsNullOrEmpty(difficulty) && difficulty != "Herhangi")
            {
                query = query.Where(q => q.DifficultyLevel == (difficulty == "1" ? DifficultyLevel.Easy : difficulty == "2" ? DifficultyLevel.Medium : DifficultyLevel.Hard));
            }

            // Get random questions
            var questions = await query
                .OrderBy(q => Guid.NewGuid()) // Random order
                .Take(questionCount)
                .Select(q => new
                {
                    q.Id,
                    q.QuestionText,
                    q.DifficultyLevel,
                    SubTopic = new { q.SubTopic.Id, q.SubTopic.Name }
                })
                .ToListAsync();

            if (!questions.Any())
            {
                return Json(new { success = false, error = "Belirtilen kriterlere uygun yeterli soru bulunamadı." });
            }

            return Json(new { success = true, questions });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    // Show exam preview before saving
    public IActionResult ExamPreview(List<QuestionBank> questions)
    {
        return View(questions);
    }

    // Save exam after preview
    [HttpPost]
    public async Task<IActionResult> SaveExam(
        string title,
        string description,
        int courseId,
        [FromForm] List<int> questionIds,
        int examDuration = 60,
        int passingScore = 60,
        bool isDraft = false,
        bool randomizeQuestions = false,
        bool showResults = true,
        bool allowReview = true)
    {
        if (questionIds == null || !questionIds.Any())
        {
            return RedirectToAction("CreateExam");
        }
        
        // Create new exam
        var exam = new Exam
        {
            CourseId = courseId,
            Title = title,
            Description = description,
            CreatedAt = DateTime.Now,
            CreatedBy = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1"),
            Status = isDraft ? ExamStatus.Draft : ExamStatus.Completed,
            ExamDuration = examDuration,
            PassingScore = passingScore,
            RandomizeQuestions = randomizeQuestions,
            ShowResults = showResults,
            AllowReview = allowReview,
            IsDraft = isDraft
        };
        
        await _context.Exams.AddAsync(exam);
        await _context.SaveChangesAsync();
        
        // Save exam questions relationship
        for (int i = 0; i < questionIds.Count; i++)
        {
            var examQuestion = new ExamQuestion
            {
                ExamId = exam.Id,
                QuestionId = questionIds[i],
                QuestionOrder = i + 1,
                CreatedAt = DateTime.Now,
            };
            
            await _context.ExamQuestions.AddAsync(examQuestion);
        }
        
        await _context.SaveChangesAsync();
        
        TempData["SuccessMessage"] = "Sınav başarıyla kaydedildi.";
        return RedirectToAction("MyExams");
    }

    // View all saved exams
    [HttpGet]
    public async Task<IActionResult> MyExams()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1");
        
        var exams = await _context.Exams
            .Include(e => e.Course)
            .Include(e => e.Questions)
                .ThenInclude(eq=>eq.Question)
            .Where(e => e.CreatedBy == userId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
            
        return View(exams);
    }
    
    // View exam details
    [HttpGet]
    public async Task<IActionResult> ExamDetail(int id)
    {
        var exam = await _context.Exams
            .Include(e => e.Course)
            .Include(e => e.Questions)
                .ThenInclude(eq => eq.Question)
                    .ThenInclude(q => q.Options)
            .Include(e => e.Questions)
                .ThenInclude(eq => eq.Question)
                    .ThenInclude(q => q.ExamQuestions)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
            
        if (exam == null)
        {
            return NotFound();
        }
        
        return View(exam);
    }
    
    // Generate PDF for exam
    [HttpGet]
    public async Task<IActionResult> DownloadExam(int id)
    {
        try
        {
            // Get exam with all necessary includes
            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.Questions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.Options)
                .Include(e => e.Questions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.SubTopic)
                            .ThenInclude(s => s.Topic)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
            {
                return NotFound($"Sınav bulunamadı (ID: {id})");
            }

            // Validate exam data
            if (exam.Questions == null || !exam.Questions.Any())
            {
                return BadRequest("Sınavda soru bulunamadı");
            }

            // Get the PDF service from DI
            var pdfService = HttpContext.RequestServices.GetRequiredService<IPdfService>();
            
            // Generate PDF from view
            var pdfBytes = await pdfService.GeneratePdfFromView("ExamPdf", exam);
            
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return StatusCode(500, "PDF oluşturulamadı");
            }

            // Return the PDF file
            var fileName = $"{exam.Title.Replace(" ", "_")}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error generating PDF: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                Console.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
            }
            
            return StatusCode(500, $"PDF oluşturulurken bir hata oluştu: {ex.Message}");
        }
    }

    // Edit exam
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var exam = await _context.Exams
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exam == null)
        {
            return NotFound();
        }

        // Load courses for dropdown
        ViewBag.Courses = await _context.Courses.ToListAsync();

        return View(exam);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Exam exam)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var existingExam = await _context.Exams.FindAsync(exam.Id);
                if (existingExam == null)
                {
                    return NotFound();
                }

                // Update only the editable fields
                existingExam.Title = exam.Title;
                existingExam.Description = exam.Description;
                existingExam.CourseId = exam.CourseId;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sınav başarıyla güncellendi.";
                return RedirectToAction(nameof(MyExams));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Exams.AnyAsync(e => e.Id == exam.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // If we got this far, something failed, redisplay form
        ViewBag.Courses = await _context.Courses.ToListAsync();
        return View(exam);
    }
    
    // Delete exam
    [Authorize]
    public IActionResult Delete()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> SearchQuestions(string searchText, int? courseId, int? topicId, int? subtopicId, string difficulty, int page = 1, int pageSize = 10)
    {
        try
        {
            var query = _context.QuestionBanks
                .Include(q => q.SubTopic)
                    .ThenInclude(s => s.Topic)
                .Include(q => q.Options)
                .AsQueryable();

            // Filtreleme
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(q => q.QuestionText.Contains(searchText));
            }

            if (courseId.HasValue)
            {
                query = query.Where(q => q.SubTopic.Topic.CourseId == courseId.Value);
            }

            if (topicId.HasValue)
            {
                query = query.Where(q => q.SubTopic.TopicId == topicId.Value);
            }

            if (subtopicId.HasValue)
            {
                query = query.Where(q => q.SubTopicId == subtopicId.Value);
            }

            if (!string.IsNullOrEmpty(difficulty) && difficulty != "Herhangi")
            {
                query = query.Where(q => q.DifficultyLevel == (difficulty == "1" ? DifficultyLevel.Easy : difficulty == "2" ? DifficultyLevel.Medium : difficulty == "3" ? DifficultyLevel.Hard : DifficultyLevel.Easy));
            }

            // Toplam kayıt sayısı
            var totalCount = await query.CountAsync();

            // Sayfalama
            var questions = await query
                .OrderByDescending(q => q.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(q => new
                {
                    q.Id,
                    q.QuestionText,
                    q.DifficultyLevel,
                    SubTopic = new { q.SubTopic.Id, q.SubTopic.Name }
                })
                .ToListAsync();

            return Json(new { questions, totalCount });
        }
        catch (Exception ex)
        {
            return Json(new { error = "Sorular yüklenirken bir hata oluştu: " + ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestionDetail(int questionId)
    {
        try
        {
            var question = await _context.QuestionBanks
                .Include(q => q.SubTopic)
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
            {
                return Json(new { error = "Soru bulunamadı." });
            }

            return Json(new
            {
                question.QuestionText,
                question.DifficultyLevel,
                SubTopic = new { question.SubTopic.Id, question.SubTopic.Name },
                Options = question.Options.Select(o => new { OptionText = o.OptionText, o.IsCorrect })
            });
        }
        catch (Exception ex)
        {
            return Json(new { error = "Soru detayları yüklenirken bir hata oluştu: " + ex.Message });
        }
    }

    // POST: Exams/DeleteExam
    [HttpPost]
    public IActionResult DeleteExam(int id)
    {
        try
        {
            var exam = _context.Exams
                .Include(e => e.Questions)
                .FirstOrDefault(e => e.Id == id);

            if (exam == null)
            {
                return Json(new { success = false, error = "Sınav bulunamadı." });
            }

            // Sınav-soru ilişkilerini sil
            _context.ExamQuestions.RemoveRange(exam.Questions);
            
            // Sınavı sil
            _context.Exams.Remove(exam);
            _context.SaveChanges();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }
}