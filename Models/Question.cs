using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class Question
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Soru metni zorunludur.")]
    public string QuestionText { get; set; }

    [Required(ErrorMessage = "Ders seçimi zorunludur.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Konu seçimi zorunludur.")]
    public int TopicId { get; set; }

    public int? SubTopicId { get; set; }

    [Required(ErrorMessage = "Soru tipi zorunludur.")]
    public QuestionType QuestionType { get; set; }

    [Required(ErrorMessage = "Doğru cevap zorunludur.")]
    public string CorrectAnswer { get; set; }

    public string? Explanation { get; set; }

    public Course Course { get; set; }
    public Topic Topic { get; set; }
    public SubTopic SubTopic { get; set; }
}

public enum QuestionType
{
    MultipleChoice = 1,
    TrueFalse = 2,
    ShortAnswer = 3,
    LongAnswer = 4
} 