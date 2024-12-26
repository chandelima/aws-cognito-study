using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.Entities;
using ToDoApp.API.Extensions;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.API.Controllers;

[Route("api/v1/todo")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IGetToDoService _getService;
    private readonly ICreateToDoService _createService;
    private readonly IUpdateToDoService _updateService;
    private readonly IDeleteToDoService _deleteService;

    public ToDoController(
        IGetToDoService getService,
        ICreateToDoService createService,
        IUpdateToDoService updateService,
        IDeleteToDoService deleteService)
    {
        _getService = getService;
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = new DafaultResponse(
            await _getService.GetAll(),
            _getService.Messages);

        return this.GetResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = new DafaultResponse(
            await _getService.GetById(id),
            _getService.Messages);

        return this.GetResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateToDoDTO? dto)
    {
        await _createService.Create(dto);
        var response = new DafaultResponse(_createService.Messages);

        return this.CreateResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateToDoDTO? dto)
    {
        await _updateService.Update(dto);
        var response = new DafaultResponse(_getService.Messages);

        return this.CreateResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = new DafaultResponse(
            await _deleteService.Delete(id),
            _getService.Messages);

        return this.DeleteResponse(response);
    }
}
