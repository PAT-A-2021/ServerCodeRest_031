using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceRest_20190140031_Bima_Hayu_Nugraha;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace ServerCodeRest_20190140031_Bima_Hayu_Nugraha
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostObjek = null;
            Uri address = new Uri("http://localhost:1907/Mahasiswa");
            WebHttpBinding binding = new WebHttpBinding();
            try
            {
                hostObjek = new ServiceHost(typeof(TI_UMY), address);
                //Alamat base address
                hostObjek.AddServiceEndpoint(typeof(ITI_UMY), binding, "");

                //alamat endpoint
                //wsdl
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();    //service runtime player
                smb.HttpGetEnabled = true;  //untuk mengaktifkan wsdl (dibuka saat development, tidak untuk dibuka)
                hostObjek.Description.Behaviors.Add(smb);
                //mex
                Binding mexbinding = MetadataExchangeBindings.CreateMexHttpBinding();
                hostObjek.AddServiceEndpoint(typeof(IMetadataExchange), mexbinding, "mex");

                WebHttpBehavior whb = new WebHttpBehavior();
                whb.HelpEnabled = true;
                hostObjek.Description.Endpoints[0].EndpointBehaviors.Add(whb);

                hostObjek.Open();
                Console.WriteLine("Server is ready...");
                Console.ReadLine();
                hostObjek.Close();
            }
            catch (Exception ex)
            {
                hostObjek = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
