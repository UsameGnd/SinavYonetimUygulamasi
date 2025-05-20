using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavYonetimUygulamasi.Context;
using SinavYonetimUygulamasi.Models;
using SinavYonetimUygulamasi.Models.ViewModels;
using System.Security.Claims;

namespace SinavYonetimUygulamasi.Controllers;

public class ExamController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ExamController> _logger;

    public ExamController(AppDbContext context, ILogger<ExamController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Create()
    {
        var viewModel = new ExamCreateViewModel
        {
            AvailableCourses = await _context.Courses.ToListAsync(),
            AvailableTopics = await _context.Topics.ToListAsync(),
            AvailableSubTopics = await _context.SubTopics.ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExamCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.AvailableCourses = await _context.Courses.ToListAsync();
            model.AvailableTopics = await _context.Topics.ToListAsync();
            model.AvailableSubTopics = await _context.SubTopics.ToListAsync();
            return View(model);
        }

        try
        {
            var exam = new Exam
            {
                Title = model.Title,
                Description = model.Description,
                CourseId = model.CourseId,
                ExamDuration = model.ExamDuration,
                PassingScore = model.PassingScore,
                RandomizeQuestions = model.RandomizeQuestions,
                ShowResults = model.ShowResults,
                AllowReview = model.AllowReview,
                IsDraft = true,
                Status = ExamStatus.Draft,
                CreatedAt = DateTime.Now,
                CreatedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            // Mode 1: Manual Question Selection
            if (model.SelectedQuestions.Any())
            {
                foreach (var question in model.SelectedQuestions)
                {
                    var examQuestion = new ExamQuestion
                    {
                        ExamId = exam.Id,
                        QuestionId = question.QuestionId,
                        QuestionOrder = 0,
                        CreatedAt = DateTime.Now
                    };
                    _context.ExamQuestions.Add(examQuestion);
                }
            }
            // Mode 2: Random Questions by Course
            else if (model.SelectedCourseId.HasValue)
            {
                var randomQuestions = await _context.QuestionBanks
                    .Where(q => q.CourseId == model.SelectedCourseId)
                    .OrderBy(r => Guid.NewGuid())
                    .Take(model.QuestionCount)
                    .ToListAsync();

                var order = 0;
                foreach (var question in randomQuestions)
                {
                    var examQuestion = new ExamQuestion
                    {
                        ExamId = exam.Id,
                        QuestionId = question.Id,
                        QuestionOrder = order++,
                        CreatedAt = DateTime.Now
                    };
                    _context.ExamQuestions.Add(examQuestion);
                }
            }
            // Mode 3: Random Questions by Course and Topic
            else if (model.SelectedTopicId.HasValue)
            {
                var randomQuestions = await _context.QuestionBanks
                    .Where(q => q.CourseId == model.CourseId && q.TopicId == model.SelectedTopicId)
                    .OrderBy(r => Guid.NewGuid())
                    .Take(model.QuestionCount)
                    .ToListAsync();

                var order = 0;
                foreach (var question in randomQuestions)
                {
                    var examQuestion = new ExamQuestion
                    {
                        ExamId = exam.Id,
                        QuestionId = question.Id,
                        QuestionOrder = order++,
                        CreatedAt = DateTime.Now
                    };
                    _context.ExamQuestions.Add(examQuestion);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sınav oluşturulurken bir hata oluştu");
            ModelState.AddModelError("", "Sınav oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.");
            model.AvailableCourses = await _context.Courses.ToListAsync();
            model.AvailableTopics = await _context.Topics.ToListAsync();
            model.AvailableSubTopics = await _context.SubTopics.ToListAsync();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions(int courseId, int? topicId = null, int? subTopicId = null)
    {
        var query = _context.QuestionBanks.Where(q => q.CourseId == courseId);

        if (topicId.HasValue)
        {
            query = query.Where(q => q.TopicId == topicId);
        }

        if (subTopicId.HasValue)
        {
            query = query.Where(q => q.SubTopicId == subTopicId);
        }

        var questions = await query
            .Select(q => new QuestionSelectionViewModel
            {
                QuestionId = q.Id,
                CourseId = q.CourseId,
                TopicId = q.TopicId,
                SubTopicId = q.SubTopicId,
                QuestionText = q.QuestionText,
                CourseName = q.Course.Name,
                TopicName = q.Topic.Name,
                SubTopicName = q.SubTopic.Name,
                DifficultyLevel = q.DifficultyLevel
            })
            .ToListAsync();

        return Json(questions);
    }

    [HttpGet]
    public async Task<IActionResult> GetTopics(int courseId)
    {
        var topics = await _context.Topics
            .Where(t => t.CourseId == courseId)
            .Select(t => new { id = t.Id, name = t.Name })
            .ToListAsync();

        return Json(topics);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubTopics(int courseId, int topicId)
    {
        var subTopics = await _context.SubTopics
            .Where(st => st.TopicId == topicId)
            .Select(st => new { id = st.Id, name = st.Name })
            .ToListAsync();

        return Json(subTopics);
    }
} 