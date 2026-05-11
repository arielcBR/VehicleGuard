namespace VehicleGuard.Api.ViewModels;

public class ResultViewModel<T>
{
    public T? Data { get; private set; }
    public IList<string> Errors { get; private set; } = new List<string>();

    // Sobrecargas construtores
    
    // Apenas um erro
    public ResultViewModel(string error) => Errors.Add(error);
    
    // Multiplos erros
    public ResultViewModel(List<string> errors) => Errors = errors;
    
    // Retorna apenas dados (sucesso)
    public ResultViewModel(T data) => Data = data;
    
    // Retorna dados e erros 
    public ResultViewModel(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }
    
    
}