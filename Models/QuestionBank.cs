using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinavYonetimUygulamasi.Models;

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}

public class QuestionBank
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ders seçimi zorunludur.")]
    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Konu seçimi zorunludur.")]
    public int TopicId { get; set; }

    [Required(ErrorMessage = "Alt konu seçimi zorunludur.")]
    public int SubTopicId { get; set; }

    [Required(ErrorMessage = "Soru metni zorunludur.")]
    public string QuestionText { get; set; } = null!;
    
    public string? ImagePath { get; set; }

    [Required(ErrorMessage = "Zorluk seviyesi zorunludur.")]
    [Range(1, 5, ErrorMessage = "Zorluk seviyesi 1-5 arasında olmalıdır.")]
    public DifficultyLevel DifficultyLevel { get; set; }
    
    [Required(ErrorMessage = "Soru tipi zorunludur.")]
    public QuestionType QuestionType { get; set; }

    public string? OptionA { get; set; }
    public string? OptionB { get; set; }
    public string? OptionC { get; set; }
    public string? OptionD { get; set; }
    public string? OptionE { get; set; }

    [Required(ErrorMessage = "Doğru cevap zorunludur.")]
    public string CorrectAnswer { get; set; } = null!;

    public string? Explanation { get; set; }

    [Required(ErrorMessage = "Puan zorunludur.")]
    [Range(0, 100, ErrorMessage = "Puan 0-100 arasında olmalıdır.")]
    public int Points { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? CreatedBy { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Topic Topic { get; set; } = null!;

    public virtual SubTopic SubTopic { get; set; } = null!;
    
    public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();

    public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
}
