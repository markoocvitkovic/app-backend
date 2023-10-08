using System.ComponentModel.DataAnnotations;
using System.Globalization;
using AstraLicenceManager.Entities;

namespace AstraLicenceManager.Dto
{
    public class LicenceInfoDto
    {
        public long Id { get; set; }
        public long? AppId { get; set; }
        public App? App { get; set; }
        public int ApplicationLevel { get; set; }
        public bool LicenceActive { get; set; }
        public LicenceStatus Status { get; set; }
        public string LicenceStatus { get { return Status.ToString(); } }
        public bool UpdateActive { get; set; }
    }

    public enum LicenceStatus
    {
        Ok,
        NotExisting,
        WrongApp,
        WrongDevice,
        ServerError,
        NotActive
    }
}
