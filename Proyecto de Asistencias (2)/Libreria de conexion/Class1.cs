using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_conexion
{
    public partial class Aprendiz
    {
        [NotMapped] // Esto asegura que este campo no se mapeará a la base de datos

        public string EstadoDescripcion
        {
            get
            {
                // Devuelve "En formación" si Estado es verdadero; de lo contrario, devuelve "Retirado".
                return (bool)Estado ? "En formación" : "Retirado";
            }
            set
            {
                // Asigna true a Estado si el valor es "En formación"; de lo contrario, asigna false.
                if (value == "En formación")
                {
                    Estado = true;
                }
                else if (value == "Retirado")
                {
                    Estado = false;
                }
            }
        }
    }

}
