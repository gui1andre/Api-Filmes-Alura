using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class CreateFilmeDto
{

    [Required(ErrorMessage = "O campo Titulo é obrigatorio!")]
    [StringLength(90, ErrorMessage = "O campo Titulo não pode exceder 90 caracteres")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O campo Genero é obrigatorio!")]
    [StringLength(15,MinimumLength = 3 ,ErrorMessage = "O campo Genero deve ter entre 3 a 15 caracteres")]
    
    public string Genero { get; set; }
    [Required(ErrorMessage = "O campo Duração é obrigatorio!")]
    [Range(23, 600, ErrorMessage = "A duração deve ser entre 23 e 600 minutos")]
    public int Duracao { get; set; }
  

}
