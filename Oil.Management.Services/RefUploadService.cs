using Oil.Management.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Services
{
    public class RefUploadService
    {
        private string RootFolder = @"C:\OilsUpload\"; 
        public RefUploadService()
        {

        }

        private void InitPathFiles()
        {
            ConstPathFilesConstant.PathFileGuaranteUpload = Path.Combine(RootFolder, @"Guarante");
            ConstPathFilesConstant.RequestFileGuaranteUpload = "/Guarante";
        }

        public void InitConstant()
        {
            InitPathFiles();
        }
    }
}
