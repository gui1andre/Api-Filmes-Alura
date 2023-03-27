using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme
{
    [Required(ErrorMessage = "O campo Titulo é obrigatorio!")]
    [MaxLength(90, ErrorMessage = "O campo Titulo não pode exceder 90 caracteres")]
      public string Titulo { get; set; }
    [Required(ErrorMessage = "O campo Genero é obrigatorio!")]
    [MaxLength(15, ErrorMessage = "O campo Genero não pode exceder 90 caracteres")]
    [MinLength(3, ErrorMessage = "O campo Genero deve ter mais de 3 caracteres")]
    public string Genero { get; set; }
    [Required(ErrorMessage = "O campo Duração é obrigatorio!")]
    [Range(23, 600, ErrorMessage = "A duração deve ser entre 23 e 600 minutos")]
    public int Duracao { get; set; }
    [Key]
    [Required]
    public int Id { get; set; }
}
