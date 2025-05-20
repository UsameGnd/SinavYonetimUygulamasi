using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinavYonetimUygulamasi.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [StringLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir.")]
    public string PasswordHash { get; set; } = null!;

    [Required(ErrorMessage = "Rol zorunludur.")]
    [StringLength(50, ErrorMessage = "Rol en fazla 50 karakter olabilir.")]
    public string Role { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<QuestionBank> Questions { get; set; } = new List<QuestionBank>();
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
