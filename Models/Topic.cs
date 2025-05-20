using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class Topic
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Konu adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Konu adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Konu açıklaması en fazla 500 karakter olabilir.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Ders seçimi zorunludur.")]
    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<SubTopic> SubTopics { get; set; } = new List<SubTopic>();
    public virtual ICollection<QuestionBank> Questions { get; set; } = new List<QuestionBank>();
}
