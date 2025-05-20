using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinavYonetimUygulamasi.Models;

public enum ExamStatus
{
    Draft = 1,
    Published = 2,
    Completed = 3,
    Cancelled = 4
}

public class Exam
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Sınav başlığı zorunludur.")]
    [StringLength(200, ErrorMessage = "Sınav başlığı en fazla 200 karakter olabilir.")]
    public string Title { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Sınav açıklaması en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Ders seçimi zorunludur.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Sınav süresi zorunludur.")]
    public int ExamDuration { get; set; }

    [Required(ErrorMessage = "Geçme notu zorunludur.")]
    public int PassingScore { get; set; }

    public bool RandomizeQuestions { get; set; }
    public bool ShowResults { get; set; }
    public bool AllowReview { get; set; }
    public bool IsDraft { get; set; }
    public ExamStatus Status { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<ExamQuestion> Questions { get; set; } = new List<ExamQuestion>();
}
