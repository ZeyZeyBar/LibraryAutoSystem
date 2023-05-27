using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class Kitaplar
{
    public int KitapId { get; set; }

    public string KitapAdi { get; set; } = null!;

    public string? YayinEvi { get; set; }

    public DateOnly? YayinTarihi { get; set; }

    public string? Tur { get; set; }

    public string? IsbnNo { get; set; }

    public int? YazarId { get; set; }

    public virtual ICollection<IadeKitaplar> IadeKitaplars { get; set; } = new List<IadeKitaplar>();

    public virtual ICollection<OduncAlinanKitaplar> OduncAlinanKitaplars { get; set; } = new List<OduncAlinanKitaplar>();

    public virtual Yazar? Yazar { get; set; }
}
