using ApiSouMaisFit.Models;

namespace ApiSouMaisFit.Services;

public interface IAlunoService
{
    //Get
    Task<IEnumerable<Aluno>> GetAlunos();
    Task<Aluno> GetAlunosById(int id);
    Task<IEnumerable<Aluno>> GetAlunosByNome(String nome);

    //Post
    Task AddAluno(Aluno aluno);

    //Update
    Task UpdateAluno(Aluno aluno);

    //Delete
    Task DeleteAluno(Aluno aluno);
}
