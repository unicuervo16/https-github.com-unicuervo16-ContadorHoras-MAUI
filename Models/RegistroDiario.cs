using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Contador_de_Horas.Models
{
    public class RegistroDiario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraIngreso { get; set; }

        public TimeSpan HoraEgreso { get; set; }

        [Ignore]
        public TimeSpan HorasTrabajadas =>
            HoraEgreso > HoraIngreso ? HoraEgreso - HoraIngreso : TimeSpan.Zero;

    }
}
