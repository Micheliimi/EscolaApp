using APIEscola.Repositorio.Interfaces;
using APIEscola.Repositorio.Repositorios;
using APIEscola.Service.Interfaces;
using APIEscola.Service.Services;
using Escola.API.Routers;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration["SqlConnection:SqlConnectionString"];

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(connectionString));

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IAlunoTurmaService, AlunoTurmaService>();

builder.Services.AddTransient<IRepositorioAluno, RepositorioAluno>();
builder.Services.AddTransient<IRepositorioTurma, RepositorioTurma>();
builder.Services.AddTransient<IRepositorioAlunoTurma, RepositorioAlunoTurma>();
var app = builder.Build();

app.UseSwagger();
//var aluno = app.NewVersionedApi();
//app.MapGroup("/aluno").MapAluno();



app.MapGroup("/v1/aluno").MapeiaAluno();
app.MapGroup("/v1/turma").MapeiaTurma();
app.MapGroup("/v1/alunoturma").MapeiaAlunoTurma();

//app.MapPost("aluno", async (AlunoModel aluno, Contexto contexto) =>
//{
//    contexto.Aluno.Add(aluno);
//    await contexto.SaveChangesAsync();
//});

//app.MapDelete("aluno/{id}", async (int id, Contexto contexto) =>
//{
//    var alunoExcluir = await contexto.Aluno.FirstOrDefaultAsync(p => p.Id == id);

//    if (alunoExcluir != null)
//    {
//        contexto.Aluno.Remove(alunoExcluir);
//        await contexto.SaveChangesAsync();
//    }   
//});

//app.MapGet("aluno", async (Contexto contexto) =>
//{
//    return await contexto.Aluno.ToListAsync();

//});

//app.MapGet("aluno/{id}", async (int id, Contexto contexto) =>
//{
//    return await contexto.Aluno.FirstOrDefaultAsync(p => p.Id == id);

//});

app.UseSwaggerUI();
app.Run();
