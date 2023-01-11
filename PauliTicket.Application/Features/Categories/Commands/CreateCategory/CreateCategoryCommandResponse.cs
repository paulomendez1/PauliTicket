using PauliTicket.Application.Features.DTOs.Category;
using PauliTicket.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse(): base()
        {

        }
        public CategoryCreationDTO Category { get; set; } = default!;
    }
}
