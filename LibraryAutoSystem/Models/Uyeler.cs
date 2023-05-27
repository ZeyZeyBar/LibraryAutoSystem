using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class Uyeler
{
    public int UyeId { get; set; }

    public string UyeAdi { get; set; } = null!;

    public string UyeSoyadi { get; set; } = null!;

    public string? EPosta { get; set; }

    public string Telefon { get; set; } = null!;

    public DateOnly? KayitTarihi { get; set; }

    public string? Sifre { get; set; }

    public virtual ICollection<IadeKitaplar> IadeKitaplars { get; set; } = new List<IadeKitaplar>();

    public virtual ICollection<OduncAlinanKitaplar> OduncAlinanKitaplars { get; set; } = new List<OduncAlinanKitaplar>();
}
