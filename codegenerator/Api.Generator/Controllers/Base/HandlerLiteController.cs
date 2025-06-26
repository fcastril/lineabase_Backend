
using CoreGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Generator.Controllers;

public class HandlerLiteController : ControllerBase

{
    public HandlerLiteController()
    {
        
    }
    protected IActionResult HandlerResponse<Data>(Data data){
        return this.Ok(new ResponseApi<Data> { Data = data, Status = true, Message = "Operation carried out successfully." });
    }
}
