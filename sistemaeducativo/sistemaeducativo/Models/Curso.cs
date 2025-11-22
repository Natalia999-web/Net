using System;
using System.Collections.Generic;

namespace sistemaeducativo.Models;

public partial class Curso
{
    public int CursoId { get; set; }

    public string NombreCurso { get; set; } = null!;

    public int Creditos { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
