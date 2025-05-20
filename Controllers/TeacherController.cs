using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavYonetimUygulamasi.Context;
using SinavYonetimUygulamasi.Models;
using SinavYonetimUygulamasi.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Controllers
{
    [Authorize(Roles = "Egitmen,Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TeacherController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // Teacher dashboard
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // Question bank main page
        public async Task<IActionResult> QuestionBank()
        {
            var questions = await _context.QuestionBanks
                .Include(q => q.SubTopic)
                .ThenInclude(s => s.Topic)
                .ThenInclude(t => t.Course)
                .Include(q => q.Options)
                .ToListAsync();
                
            // Add courses to ViewBag for filtering
            ViewBag.Courses = await _context.Courses.ToListAsync();
                
            return View(questions);
        }

        // Add question form
        public async Task<IActionResult> AddQuestion()
        {
            ViewBag.Courses = await _context.Courses.ToListAsync();
            
            var model = new QuestionViewModel();
            // 4 boş seçenek ekleyelim
            for (int i = 0; i < 4; i++) 
            {
                model.Options.Add(new OptionViewModel { OptionText = "" });
            }
            
            return View(model);
        }

        // Get question form based on question type
        [HttpGet]
        public async Task<IActionResult> AddQuestionByType(QuestionType questionType)
        {
            ViewBag.Courses = await _context.Courses.ToListAsync();
            ViewBag.SelectedQuestionType = questionType;
            
            var model = new QuestionViewModel
            {
                QuestionType = questionType
            };
            
            // Add appropriate number of options based on question type
            switch (questionType)
            {
                case QuestionType.MultipleChoice:
                    for (int i = 0; i < 4; i++)
                    {
                        model.Options.Add(new OptionViewModel { OptionText = "" });
                    }
                    break;
                    
                case QuestionType.TrueFalse:
                    model.Options.Add(new OptionViewModel { OptionText = "Doğru" });
                    model.Options.Add(new OptionViewModel { OptionText = "Yanlış" });
                    break;
                    
                case QuestionType.ShortAnswer:
                    // No options needed
                    break;
                    
                case QuestionType.LongAnswer:
                    // No options needed
                    break;
            }
            
            return View("AddQuestion", model);
        }

        // Save question to database
        [HttpPost]
        public async Task<IActionResult> SaveQuestion(QuestionViewModel model)
        {
            try
            {
                // Soru tipine göre validasyon hatalarını temizle
                if (model.QuestionType != QuestionType.ShortAnswer)
                {
                    ModelState.Remove("FillInBlankAnswer");
                }
                if (model.QuestionType != QuestionType.TrueFalse)
                {
                    ModelState.Remove("TrueFalseAnswer");
                }
                if (model.QuestionType != QuestionType.MultipleChoice)
                {
                    ModelState.Remove("CorrectOptionIndex");
                    ModelState.Remove("Options");
                }

                // Model validasyonu
                var validationContext = new ValidationContext(model);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError("", validationResult.ErrorMessage);
                    }
                    ViewBag.Courses = await _context.Courses.ToListAsync();
                    return View("AddQuestion", model);
                }

                var question = new QuestionBank
                {
                    CourseId = model.CourseId,
                    TopicId = model.TopicId,
                    SubTopicId = model.SubtopicId,
                    QuestionText = model.QuestionText,
                    DifficultyLevel = model.Difficulty == "1" ? DifficultyLevel.Easy : model.Difficulty == "2" ? DifficultyLevel.Medium : DifficultyLevel.Hard,
                    QuestionType = model.QuestionType,
                    CreatedAt = DateTime.Now
                };

                // Resim yükleme işlemi
                if (model.QuestionImage != null)
                {
                    var fileName = Path.GetFileName(model.QuestionImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.QuestionImage.CopyToAsync(stream);
                    }
                    question.ImagePath = "/uploads/" + fileName;
                }

                _context.QuestionBanks.Add(question);
                await _context.SaveChangesAsync();

                // Seçenekleri ekle
                if (model.QuestionType == QuestionType.MultipleChoice)
                {
                    for (int i = 0; i < model.Options.Count; i++)
                    {
                        var option = new QuestionOption
                        {
                            QuestionId = question.Id,
                            OptionText = model.Options[i].OptionText,
                            IsCorrect = i == model.CorrectOptionIndex,
                            OptionOrder = i,
                            CreatedAt = DateTime.Now
                        };
                        _context.QuestionOptions.Add(option);
                    }
                }
                else if (model.QuestionType == QuestionType.TrueFalse)
                {
                    var option = new QuestionOption
                    {
                        QuestionId = question.Id,
                        OptionText = model.TrueFalseAnswer == true ? "Doğru" : "Yanlış",
                        IsCorrect = true,
                        OptionOrder = 0,
                        CreatedAt = DateTime.Now
                    };
                    _context.QuestionOptions.Add(option);
                }
                else if (model.QuestionType == QuestionType.ShortAnswer)
                {
                    var option = new QuestionOption
                    {
                        QuestionId = question.Id,
                        OptionText = model.FillInBlankAnswer,
                        IsCorrect = true,
                        OptionOrder = 0,
                        CreatedAt = DateTime.Now
                    };
                    _context.QuestionOptions.Add(option);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Soru başarıyla eklendi.";
                return RedirectToAction(nameof(QuestionBank));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Soru eklenirken bir hata oluştu: " + ex.Message;
                ViewBag.Courses = await _context.Courses.ToListAsync();
                return View("AddQuestion", model);
            }
        }

        // Get topics for a specific course (AJAX)
        [HttpGet]
        public async Task<JsonResult> GetTopics(int courseId)
        {
            var topics = await _context.Topics
                .Where(t => t.CourseId == courseId)
                .Select(t => new { t.Id, t.Name })
                .ToListAsync();
                
            return Json(topics);
        }

        // Get subtopics for a specific topic (AJAX)
        [HttpGet]
        public async Task<JsonResult> GetSubtopics(int topicId)
        {
            var subtopics = await _context.SubTopics
                .Where(s => s.TopicId == topicId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(subtopics);
        }

        // Delete question from database
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await _context.QuestionBanks
                    .Include(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (question == null)
                {
                    return Json(new { success = false, message = "Soru bulunamadı." });
                }

                // Delete associated options first
                _context.QuestionOptions.RemoveRange(question.Options);

                // Delete the question
                _context.QuestionBanks.Remove(question);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Soru başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Soru silinirken bir hata oluştu: " + ex.Message });
            }
        }
    }
} 