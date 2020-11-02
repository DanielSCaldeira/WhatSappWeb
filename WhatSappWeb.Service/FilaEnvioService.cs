using System;
using System.Collections.Generic;
using System.Linq;
using WhatSappDatabase.MongoDB.Base;
using WhatSappWeb.DTO;
using WhatSappWeb.Model;

namespace WhatSappWeb.Service
{

    public class FilaEnvioService : ServiceMongo<FilaEnvio>
    {
        public async void Salvar(FilaEnvio fila)
        {
            try
            {
                await InserirAsync(fila);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel salvar o QrCode Error:" + ex);
            }
        }

        public void Inserir(List<FilaEnvioDTO> registro)
        {

            var model = registro.Select(x => x.ToModel<FilaEnvio, FilaEnvioDTO, FilaEnvio>(Colecao));
            InserirAsync(model);
        }
    }
}
