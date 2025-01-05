using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.Entities;

namespace ToDoApp.API.Config;

public static class ControllerBaseExtensions
{
    private static IActionResult OkOrBadRequestResponse(
        ControllerBase controller, 
        DafaultResponse response, 
        bool isDataNullBadRequest)
    {
        if ((isDataNullBadRequest && response.Data == null) || response.Messages?.Count > 0)
        {
            return controller.BadRequest(response);
        }

        return controller.Ok(response);
    }

    public static IActionResult CreateResponse(this ControllerBase controller, DafaultResponse response)
    {
        if (response.Messages == null || response.Messages.Count == 0)
        {
            return controller.StatusCode(StatusCodes.Status201Created, response);
        }

        return controller.BadRequest(response);
    }

    public static IActionResult GetResponse(this ControllerBase controller, DafaultResponse response)
    {
        if (response.Data != null)
        {
            return controller.Ok(response);
        }

        if (response.Messages == null || response.Messages.Count == 0)
        {
            return controller.NotFound();
        }

        return controller.BadRequest(response);
    }

    public static IActionResult ProcessResponse(this ControllerBase controller, DafaultResponse response)
    {
        return OkOrBadRequestResponse(controller, response, true);
    }

    public static IActionResult UpdateResponse(this ControllerBase controller, DafaultResponse response)
    {
        return OkOrBadRequestResponse(controller, response, false);
    }

    public static IActionResult DeleteResponse(this ControllerBase controller, DafaultResponse response)
    {
        if (response.Data)
        {
            return controller.NoContent();
        }

        return controller.BadRequest(response.Messages);
    }
}
