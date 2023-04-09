using CB.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CB.Domain.Entities.Mail
{
    public class MailTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Body { get; set; }
        public int EmailAccountId { get; set; }
        public EmailAccount EmailAccount { get; set; }
        public bool Active { get; set; }
    }
}
