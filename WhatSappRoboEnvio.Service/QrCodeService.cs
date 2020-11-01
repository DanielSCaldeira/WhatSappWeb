using System;
using System.Threading.Tasks;
using WhatSappDatabase.MongoDB.Base;
using WhatSappWeb.Model;

namespace WhatSappRoboEnvio.Service
{
    public class QrCodeService : ServiceMongo<QrCode>
    {
        public async void Salvar(QrCode qrcode)
        {
            try
            {
                await GravarAsync(qrcode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel salvar o QrCode Error:" + ex);
            }
        }
    }
}
