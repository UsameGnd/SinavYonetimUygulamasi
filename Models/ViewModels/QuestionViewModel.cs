using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SinavYonetimUygulamasi.Models.ViewModels
{
    public class QuestionViewModel : IValidatableObject
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ders seçimi zorunludur.")]
        [Display(Name = "Ders")]
        public int CourseId { get; set; }
        
        [Required(ErrorMessage = "Konu seçimi zorunludur.")]
        [Display(Name = "Konu")]
        public int TopicId { get; set; }
        
        [Required(ErrorMessage = "Alt konu seçimi zorunludur.")]
        [Display(Name = "Alt Konu")]
        public int SubtopicId { get; set; }
        
        [Required(ErrorMessage = "Soru metni zorunludur.")]
        [Display(Name = "Soru Metni")]
        public string QuestionText { get; set; }
        
        [Display(Name = "Soru Resmi")]
        public IFormFile? QuestionImage { get; set; }
        
        [Required(ErrorMessage = "Zorluk seviyesi seçimi zorunludur.")]
        [Display(Name = "Zorluk Seviyesi")]
        public string Difficulty { get; set; }
        
        [Required(ErrorMessage = "Soru tipi seçimi zorunludur.")]
        [Display(Name = "Soru Tipi")]
        public QuestionType QuestionType { get; set; }
        
        [Display(Name = "Doğru Şık")]
        public int? CorrectOptionIndex { get; set; }
        
        [Display(Name = "Doğru/Yanlış Cevap")]
        public bool? TrueFalseAnswer { get; set; }
        
        [Display(Name = "Doğru Cevap (Boşluk Doldurma)")]
        public string FillInBlankAnswer { get; set; }
        
        public List<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            switch (QuestionType)
            {
                case QuestionType.MultipleChoice:
                    if (CorrectOptionIndex == null)
                    {
                        results.Add(new ValidationResult("Lütfen doğru cevabı seçin.", new[] { nameof(CorrectOptionIndex) }));
                    }
                    if (Options == null || Options.Count < 2)
                    {
                        results.Add(new ValidationResult("En az 2 seçenek girmelisiniz.", new[] { nameof(Options) }));
                    }
                    if (Options?.Any(o => string.IsNullOrWhiteSpace(o.OptionText)) == true)
                    {
                        results.Add(new ValidationResult("Tüm seçenekleri doldurun.", new[] { nameof(Options) }));
                    }
                    if (QuestionImage == null)
                    {
                        results.Add(new ValidationResult("Çoktan seçmeli sorular için görsel yüklemek zorunludur.", new[] { nameof(QuestionImage) }));
                    }
                    break;

                case QuestionType.TrueFalse:
                    if (TrueFalseAnswer == null)
                    {
                        results.Add(new ValidationResult("Lütfen doğru cevabı seçin.", new[] { nameof(TrueFalseAnswer) }));
                    }
                    break;

                case QuestionType.LongAnswer:
                    if (string.IsNullOrWhiteSpace(FillInBlankAnswer))
                    {
                        results.Add(new ValidationResult("Lütfen doğru cevabı girin.", new[] { nameof(FillInBlankAnswer) }));
                    }
                    break;
            }

            return results;
        }
    }
    
    public class OptionViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Şık metni zorunludur.")]
        public string OptionText { get; set; }
        
        public bool IsCorrect { get; set; }
    }
} 