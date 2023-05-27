using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class Yazar
{
    public int YazarId { get; set; }

    public string YazarAdi { get; set; } = null!;

    public string YazarSoyadi { get; set; } = null!;

    public DateOnly? DogumTarihi { get; set; }

    public string? YazarUlke { get; set; }

    public virtual ICollection<Kitaplar> Kitaplars { get; set; } = new List<Kitaplar>();
}
