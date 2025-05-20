using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinavYonetimUygulamasi.Models;

public class ExamQuestion
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ExamId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    public int QuestionOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ExamId")]
    public virtual Exam Exam { get; set; } = null!;

    [ForeignKey("QuestionId")]
    public virtual QuestionBank Question { get; set; } = null!;
} 