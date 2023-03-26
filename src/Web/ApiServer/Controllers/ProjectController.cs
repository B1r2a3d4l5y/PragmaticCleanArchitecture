﻿using ApiServer.Identity;
using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Application.Projects.Commands;
using Application.Projects.Queries;
using Application.Projects.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    public class ProjectController : CustomControllerBase
    {
        private readonly IMapper<ProjectsViewModel, IEnumerable<Project>> _mapper;

        public ProjectController(
            IMediator mediator,
            IMapper<ProjectsViewModel, IEnumerable<Project>> mapper) : base(mediator)
        {
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize(Permission.WriteProjects)]
        public async Task<ActionResult<int>> PostProject(
            CreateProjectRequest request)
        {
            return await Mediator.Send(new CreateProjectCommand(request));
        }

        [HttpPut("{id}")]
        [Authorize(Permission.WriteProjects)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutProject(int id,
            UpdateProjectRequest request)
        {
            if (id != request.Id) return BadRequest();

            await Mediator.Send(new UpdateProjectCommand(request));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Permission.WriteProjects)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await Mediator.Send(new DeleteProjectCommand(id));

            return NoContent();
        }

        [HttpGet]
        [Authorize(Permission.ReadProjects)]
        public async Task<ActionResult<ProjectsViewModel>> GetProjects()
        {
            return _mapper.Map(await Mediator.Send(new GetProjectsQuery()));
        }
    }
}
