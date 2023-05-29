using Npgsql;
using System.Data;

namespace LibraryAutoSystem.Models
{
    public class FunctionsCall
    {
        private readonly LibraryContext _context;
        public FunctionsCall(LibraryContext context)
        {
            _context = context;
        }
        public void Functions()
        {
            var yazar=_context.Yazars.ToList();
            
            using(var cn = GetConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand("yazar_kitaplari",cn);
                //cmd.Parameters.AddWithValue(new NpgsqlParameter("soyadi",DbType.String)).Value=yazar.Where(x=>x.YazarSoyadi==);
                cmd.CommandType = CommandType.StoredProcedure;
                var reader=cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader);
                }
            }
        }
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Host = localhost; Database = Library; Username = postgres; Password = 123456");
        }
    }
}
