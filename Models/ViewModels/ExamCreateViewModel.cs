using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models.ViewModels;

public class ExamCreateViewModel
{
    [Required(ErrorMessage = "Sınav başlığı zorunludur.")]
    [StringLength(200, ErrorMessage = "Sınav başlığı en fazla 200 karakter olabilir.")]
    public string Title { get; set; }

    [StringLength(500, ErrorMessage = "Sınav açıklaması en fazla 500 karakter olabilir.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Ders seçimi zorunludur.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Sınav süresi zorunludur.")]
    [Range(1, 480, ErrorMessage = "Sınav süresi 1-480 dakika arasında olmalıdır.")]
    public int ExamDuration { get; set; }

    [Required(ErrorMessage = "Geçme notu zorunludur.")]
    [Range(0, 100, ErrorMessage = "Geçme notu 0-100 arasında olmalıdır.")]
    public int PassingScore { get; set; }

    [Required(ErrorMessage = "Soru sayısı zorunludur.")]
    [Range(1, 100, ErrorMessage = "Soru sayısı 1-100 arasında olmalıdır.")]
    public int QuestionCount { get; set; }

    public bool RandomizeQuestions { get; set; } = true;
    public bool ShowResults { get; set; } = true;
    public bool AllowReview { get; set; } = true;

    // Mode 1: Manual Question Selection
    public List<QuestionSelectionViewModel> SelectedQuestions { get; set; } = new();

    // Mode 2: Random Questions by Course
    public int? SelectedCourseId { get; set; }

    // Mode 3: Random Questions by Course and Topic
    public int? SelectedTopicId { get; set; }

    // Navigation properties
    public List<Course> AvailableCourses { get; set; } = new();
    public List<Topic> AvailableTopics { get; set; } = new();
    public List<SubTopic> AvailableSubTopics { get; set; } = new();
}

public class QuestionSelectionViewModel
{
    public int QuestionId { get; set; }
    public int CourseId { get; set; }
    public int TopicId { get; set; }
    public int? SubTopicId { get; set; }
    public string QuestionText { get; set; }
    public string CourseName { get; set; }
    public string TopicName { get; set; }
    public string SubTopicName { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
} 