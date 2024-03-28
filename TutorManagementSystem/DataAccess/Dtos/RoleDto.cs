using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? RoleDesc { get; set; }

        public string? Status { get; set; }
    }

    public class RoleRequestDto
    {
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }
}
