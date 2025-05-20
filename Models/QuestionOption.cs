using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class QuestionOption
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Soru seçimi zorunludur.")]
    public int QuestionId { get; set; }
    
    [Required(ErrorMessage = "Seçenek metni zorunludur.")]
    [StringLength(500, ErrorMessage = "Seçenek metni en fazla 500 karakter olabilir.")]
    public string OptionText { get; set; } = null!;
    
    public bool IsCorrect { get; set; }
    
    public int OptionOrder { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public virtual QuestionBank Question { get; set; } = null!;
} 