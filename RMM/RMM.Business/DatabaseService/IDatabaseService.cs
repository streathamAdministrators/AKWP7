using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.DatabaseService
{
   public interface IDatabaseService
    {
       /// <summary>
       /// Initialize la base de donnée. Peut etre utiliser apres restauration et en debut d'installation d'application.
       /// </summary>
       /// <returns>Retourne un booleen permettant de savoir si la base est deja creer ou non. TRUE pour deja crée. </returns>
       bool Initialize();
    }
}
