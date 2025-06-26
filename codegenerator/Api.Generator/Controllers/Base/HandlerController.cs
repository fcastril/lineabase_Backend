
using CoreGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Generator.Controllers;

[AllowAnonymous]
public class HandlerController<ENT,DTO> : HandlerLiteController
 where ENT : class, new()
 where DTO : class, new()

{
    public IBaseBL<ENT,DTO> IbusinessBL { get; set; }
    public HandlerController(IBaseBL<ENT,DTO> ibusinessBL)
    {
        IbusinessBL = ibusinessBL;
    }
        [HttpPost("create")]
        public async Task<IActionResult> Create(DTO dto)
        {
            await IbusinessBL.Create(dto);
            return this.HandlerResponse(dto); 
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(DTO dto)
        {
            await IbusinessBL.Update(dto);
            return this.HandlerResponse(dto);  
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return this.HandlerResponse(await IbusinessBL.ToList());  
        }

         [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await IbusinessBL.Delete(id);
            return this.HandlerResponse(id);  
        }
}
