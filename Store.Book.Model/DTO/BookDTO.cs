using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model.DTO
{
    public class CreateBookDto
    {
        [Display(Name="Описание книги")]
        [StringLength(256, MinimumLength = 15)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Невалидный год")]
        [Display(Name ="Год выпуска книги")]
        [Range(868, 2021)]
        public int Year { get; set; }
    }

    public class BookDTO
    {
        [Display(Name ="Идентификатор")]
        public int Id { get; set; }
        [Display(Name = "Описание")]
        public string Title { get; set; }
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }
    }
}
