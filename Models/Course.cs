using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ders adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Ders adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Ders açıklaması en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

    public virtual ICollection<QuestionBank> Questions { get; set; } = new List<QuestionBank>();
}
