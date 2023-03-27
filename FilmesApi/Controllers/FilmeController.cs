using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("filme")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto newFilmeDto)
    {   

        Filme filme = _mapper.Map<Filme>(newFilmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarFilmePorId), new {id= filme.Id},filme);


    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50) {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorId(int id)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(filmes => filmes.Id == id);
        if( filme == null )return NotFound();
        ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(filme =>  filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();

    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> path)
    {
        Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        UpdateFilmeDto filmeAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        path.ApplyTo(filmeAtualizar, ModelState);
        if(!TryValidateModel(filmeAtualizar)) return ValidationProblem(ModelState);
        _mapper.Map(filmeAtualizar, filme);
        _context.SaveChanges();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id) {
        Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if(filme == null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

}
