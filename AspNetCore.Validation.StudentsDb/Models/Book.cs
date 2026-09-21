using System.ComponentModel.DataAnnotations;
using AspNetCore.Validation.StudentsDb.Annotations;

namespace AspNetCore.Validation.StudentsDb.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина має бути від 3 до 50 символів.")]
    [Display(Name = "Назва")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Display(Name = "Автор")]
    [MyAuthors(["Шилдт", "Троєлсен", "Нейгел", "Ріхтер", "Страуструп"], ErrorMessage = "Недопустимий автор.")]
    public required string Author { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Display(Name = "Рік видання")]
    public int Year { get; set; }
}