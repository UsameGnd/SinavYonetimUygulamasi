using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class SubTopic
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Alt konu adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Alt konu adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Alt konu açıklaması en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Konu seçimi zorunludur.")]
    public int TopicId { get; set; }

    public virtual Topic Topic { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<QuestionBank> Questions { get; set; } = new List<QuestionBank>();
}
